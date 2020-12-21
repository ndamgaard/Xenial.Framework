﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Xenial.Framework.Utils.Slugger
{
    /// <summary>
    /// Class Slugifier.
    /// Implements the <see cref="ISlugify" />
    /// </summary>
    /// <seealso cref="ISlugify" />
    /// <autogeneratedoc />
    public class Slugifier : ISlugify
    {
        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        /// <autogeneratedoc />
        protected SlugifierConfig Config { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slugifier"/> class.
        /// </summary>
        /// <autogeneratedoc />
        public Slugifier() : this(new SlugifierConfig()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slugifier"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <exception cref="ArgumentNullException">config - can't be null use default config or empty constructor.</exception>
        /// <autogeneratedoc />
        public Slugifier(SlugifierConfig config)
            => Config = config ?? throw new ArgumentNullException(nameof(config), "can't be null use default config or empty constructor.");

        /// <summary>
        /// Implements <see cref="ISlugify.GenerateSlug(string)"/>
        /// </summary>
        public string GenerateSlug(string inputString)
        {
            if (Config.ForceLowerCase)
            {
                inputString = inputString.ToLower();
            }

            if (Config.TrimWhitespace)
            {
                inputString = inputString.Trim();
            }

            inputString = CleanWhiteSpace(inputString, Config.CollapseWhiteSpace);
            inputString = ApplyReplacements(inputString, Config.StringReplacements);
            inputString = RemoveDiacritics(inputString);
            inputString = DeleteCharacters(inputString, Config.DeniedCharactersRegex);

            if (Config.CollapseDashes)
            {
                inputString = Regex.Replace(inputString, "--+", "-");
            }

            return inputString;
        }

        /// <summary>
        /// Cleans the white space.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="collapse">if set to <c>true</c> [collapse].</param>
        /// <returns>System.String.</returns>
        /// <autogeneratedoc />
        protected string CleanWhiteSpace(string str, bool collapse)
            => Regex.Replace(str, collapse ? @"\s+" : @"\s", " ");

        // Thanks http://stackoverflow.com/a/249126!
        /// <summary>
        /// Removes the diacritics.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        /// <autogeneratedoc />
        protected string RemoveDiacritics(string str)
        {
            var stFormD = str.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            for (var ich = 0; ich < stFormD.Length; ich++)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Applies the replacements.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="replacements">The replacements.</param>
        /// <returns>System.String.</returns>
        /// <autogeneratedoc />
        protected string ApplyReplacements(string str, Dictionary<string, string> replacements)
        {
            var sb = new StringBuilder(str);

            foreach (var replacement in replacements)
            {
                sb = sb.Replace(replacement.Key, replacement.Value);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Deletes the characters.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="regex">The regex.</param>
        /// <returns>System.String.</returns>
        /// <autogeneratedoc />
        protected string DeleteCharacters(string str, string regex)
            => Regex.Replace(str, regex, "");
    }
}

