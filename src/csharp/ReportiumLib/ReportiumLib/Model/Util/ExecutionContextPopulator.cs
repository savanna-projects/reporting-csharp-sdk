using Reportium.Client;
using Reportium.Connections;
using Reportium.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Reportium.Model.Util
{
    /// <summary>
    /// Service for automatically updating missing details in <see cref="Job"/> and <see cref="Project"/>
    /// instances from environment variables
    /// </summary>
    internal static class ExecutionContextPopulator
    {
        // constants
        private const string Comma = ",";
        private const string Equal = "=";
        private const string InvalidCustomFieldError = "Failed to parse custom fields parameter: '{0}'";

        // TODO: shortcut all other constants
        private const string VersionNameV1 = Constants.Sdk.ProjectVersionParameterNameV1;
        private const string VersionNameV2 = Constants.Sdk.ProjectVersionParameterNameV2;

        /// <summary>
        /// Returns a new {@link Job} with the same properties as the given source
        /// </summary>
        /// <remarks>
        /// If the source is missing some properties then this method will try to get values for
        /// them based on well-defined environment variable names.
        /// </remarks>
        /// <param name="src">Job source</param>
        /// <returns><c>null</c> if the interpolated job name is empty</returns>
        public static Job PopulateMissingJobPropertiesFromEnvVariables(Job src)
        {
            // setup
            var target = new Job();

            // Copy initial values
            if (src != null)
            {
                target.Number = src.Number;
                target.Name = src.Name;
                target.Branch = src.Branch;
            }

            // Fill missing properties from environment variables
            if (target.Number == 0)
            {
                string variable = EnvironmentVariableUtils
                    .GetEnvironmentVariable(Constants.Sdk.JobNumberParameterNameV2, Constants.Sdk.JobNumberParameterNameV1);

                target.Number = !string.IsNullOrEmpty(variable) ? int.Parse(variable) : 0;
            }

            if (string.IsNullOrEmpty(target.Branch))
            {
                string variable = EnvironmentVariableUtils
                    .GetEnvironmentVariable(Constants.Sdk.JobBranchParameterBranchV2, Constants.Sdk.JobBranchParameterBranchV1);

                target.Branch = !string.IsNullOrEmpty(variable) ? variable : target.Branch;
            }

            if (string.IsNullOrEmpty(target.Name))
            {
                target.Name = EnvironmentVariableUtils
                    .GetEnvironmentVariable(Constants.Sdk.JobNameParameterNameV2, Constants.Sdk.JobNameParameterNameV1);
            }

            // get
            return string.IsNullOrEmpty(target.Name) ? null : target;
        }

        /// <summary>
        /// Returns a new <see cref="Project"/> with the same properties as the given source.
        /// </summary>
        /// <remarks>
        /// If the source is missing some properties then this method will try to get values for
        /// them based on well-defined environment variable names.
        /// </remarks>
        /// <param name="src">Project source</param>
        /// <returns><c>null</c> if the interpolated project name is empty</returns>
        public static Project PopulateMissingProjectPropertiesFromEnvVariables(Project src)
        {
            // setup
            var target = new Project();

            // Copy initial values
            if (src != null)
            {
                target.Version = src.Version;
                target.Name = src.Name;
            }

            // Fill missing properties from environment variables
            if (string.IsNullOrEmpty(target.Version))
            {
                target.Version = EnvironmentVariableUtils.GetEnvironmentVariable(VersionNameV2, VersionNameV1);
            }

            if (string.IsNullOrEmpty(target.Name))
            {
                target.Name = EnvironmentVariableUtils
                    .GetEnvironmentVariable(Constants.Sdk.ProjectNameParameterNameV2, Constants.Sdk.ProjectNameParameterNameV1);
            }

            // get
            return string.IsNullOrEmpty(target.Name) ? null : target;
        }

        /// <summary>
        /// Returns a new Set&lt;{@link CustomField}&gt; with both properties as the given source and processed values
        /// from a set of predefined environment variables. If the source is missing some properties then this method will try to get values for
        /// them based on well-defined environment variable names. In case of name duplication the SRC will be preferred.
        /// </summary>
        /// <param name="src">CustomField</param>
        /// <returns><c>new Set&lt;CustomField&l</c> based on both give SRC and Environment variables</returns>
        public static ICollection<CustomField> PopulateMissingCustomFieldsPropertiesFromEnvVariables(ICollection<CustomField> src)
        {
            // setup
            var target = new List<CustomField>();
            var variable = EnvironmentVariableUtils.GetEnvironmentVariable(Constants.Sdk.CustomFieldsParameterName);
            var variablesNames = new List<string>();

            if (string.IsNullOrEmpty(variable))
            {
                var range = variable?
                    .Split(Comma[0])
                    .Select(i => GetOne(i, variable))
                    .Select(i => new CustomField(name: i.Name, value: i.Value));

                target.AddRange(range);
                variablesNames.AddRange(range.Select(i => i.Name));
            }

            // exit conditions
            if (src == null)
            {
                return target;
            }

            // build
            var customFields = src.Where(i => !variablesNames.Contains(i.Name));
            target.AddRange(customFields);

            // get
            return target;
        }

        private static (string Name, string Value) GetOne(string item, string variable)
        {
            // setup
            item = item.Trim();

            //  exit conditions
            if (string.IsNullOrEmpty(item) || !item.Contains(Equal))
            {
                throw new ReportiumException(string.Format(InvalidCustomFieldError, variable));
            }

            // build
            var nameAndValue = item.Split(Equal[0]);
            var name = nameAndValue[0].Trim();
            var value = nameAndValue.Length > 1 ? nameAndValue[1].Trim() : null;

            // get
            return (name, value);
        }

        /// <summary>
        /// Returns a new <see cref="Connection"/> based on well-defined environment variable names.
        /// </summary>
        /// <returns>  New connection based on well-defined environment variable names. </returns>
        public static Connection PopulateConnectionFromEnvVariables()
        {
            // setup
            var apiKey = Environment.GetEnvironmentVariable(Constants.Sdk.ConnectionApiKeyParameterName);
            var tenant = Environment.GetEnvironmentVariable(Constants.Sdk.ConnectionTenantParameterName);

            // get
            return new Connection(apiKey, tenant);
        }
    }
}