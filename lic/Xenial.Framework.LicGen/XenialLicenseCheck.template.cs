﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Standard.Licensing;
using Standard.Licensing.Validation;

#nullable enable

namespace Xenial
{
    /// <summary>
    /// Class Xenial__XenialModule__LicenseCheck.
    /// Used to unlock license restrictions
    /// </summary>
    /// <autogeneratedoc />
    public static class Xenial__XenialModule__LicenseCheck
    {
        private class XenialLicenseFatalFailureException : Exception
        {
            public XenialLicenseFatalFailureException(string? hint = null) : base(FormatMessage(hint)) { }
            public XenialLicenseFatalFailureException(string? hint, Exception innerException) : base(FormatMessage(hint), innerException) { }

            private static string FormatMessage(string? hint) => $"You must call {nameof(Xenial__XenialModule__LicenseCheck)}.{nameof(Xenial__XenialModule__LicenseCheck.LoadLicense)} before doing any license checking. Hint: {hint}";
        }

        private static License? license;
        internal static License License
        {
            get
            {
                _ = license ?? throw new XenialLicenseFatalFailureException($"Don't use {nameof(License)} before calling {nameof(LoadLicense)}");
                return license;
            }
            set => license = value;
        }

        /// <summary>
        /// Unlock the license using a string
        /// </summary>
        /// <param name="licenseString"></param>
        public static void LoadLicense(string licenseString)
        {
            try
            {
                License = License.Load(licenseString);
            }
            catch (Exception ex) { throw new XenialLicenseFatalFailureException("License is malformed", ex); }
        }

        /// <summary>
        /// Unlock the license using a stream
        /// </summary>
        /// <param name="licenseStream"></param>
        public static void LoadLicense(Stream licenseStream)
        {
            try
            {
                License = License.Load(licenseStream);
            }
            catch (Exception ex) { throw new XenialLicenseFatalFailureException("License is malformed", ex); }
        }

        /// <summary>
        /// Unlock the license using the manifest resource stream of the executable
        /// </summary>
        public static void LoadLicense()
        {
            if(license != null)
            {
                return;
            }

            var executablePath = XenialProcessExtensions.ExecutablePath;
            var targetAssembly = System.Reflection.Assembly.LoadFile(executablePath);
            _ = targetAssembly ?? throw new XenialLicenseFatalFailureException($"Could not load Assembly from '{executablePath}'");

            var manifestResourceStreamName = $"{targetAssembly.GetName().Name}.Xenial.License.xml";
            var licStream = targetAssembly.GetManifestResourceStream(manifestResourceStreamName);
            _ = licStream ?? throw new XenialLicenseFatalFailureException($"Could not load LicenseStream from '{manifestResourceStreamName}' in Assembly '{executablePath}'");

            LoadLicense(licStream);
        }

        internal static bool IsTrial => license?.Type != LicenseType.Standard;

        private static string ProductName => "%ProductName%";
        private static string PulicKeyToken => "%PulicKeyToken%";

        internal static bool IsSignatureValid =>
            !Validations.OfType<InvalidSignatureValidationFailure>().Any();

        internal static bool ContainsProduct =>
            !Validations.OfType<ProductMissingValidationFailure>().Any();

        internal static bool IsValid =>
            !IsTrial
            && IsSignatureValid
            && ContainsProduct;

        private class ProductMissingValidationFailure : GeneralValidationFailure
        {
            public ProductMissingValidationFailure(string productName)
            {
                Message = $"License does not include Product: '{productName}'";
                HowToResolve = $"Obtain a license that contains the Product: '{productName}'";
            }
        }

        private static IEnumerable<IValidationFailure> Validations =>
            License
            .Validate()
            .AssertThat(p => p.ProductFeatures.Contains(ProductName), new ProductMissingValidationFailure(ProductName))
            .And()
            .Signature(PulicKeyToken)
            .AssertValidLicense();
    }
}