namespace Reportium.Client
{
    /// <summary>
    /// Constants class
    /// </summary>
    internal static class Constants
    {
        public const string  BaseReportiumPath = "/test-execution-management-webapp/rest/";

        public static class Sdk
        {
            public const string TagsParameterNameV1 = "reportium-tags";
            public const string TagsParameterNameV2 = "ReportiumTags";
            public const string CustomFieldsParameterName = "ReportiumCustomFields";

            // job properties for missing properties
            public const string JobNumberParameterNameV1 = "reportium-job-number";
            public const string JobNumberParameterNameV2 = "ReportiumJobNumber";
            public const string JobNameParameterNameV1 = "reportium-job-name";
            public const string JobNameParameterNameV2 = "ReportiumJobName";
            public const string JobBranchParameterBranchV1 = "reportium-job-branch";
            public const string JobBranchParameterBranchV2 = "ReportiumJobBranch";

            // project properties for missing properties
            public const string ProjectVersionParameterNameV1 = "reportium-project-version";
            public const string ProjectVersionParameterNameV2 = "ReportiumProjectVersion";
            public const string ProjectNameParameterNameV1 = "reportium-project-name";
            public const string ProjectNameParameterNameV2 = "ReportiumProjectName";

            public const string ConnectionUriParameterName = "reportium-connection-uri";
            public const string ConnectionApiKeyParameterName = "reportium-connection-api-key";
            public const string ConnectionTenantParameterName = "reportium-connection-tenant";
        }

        public static class Capabilities
        {
            public const string ExecutionReportUrl = "testGridReportUrl";
            public const string ExecutionId = "executionId";
        }

        public static class Url
        {
            // Path format
            public const string Path = "/auth/realms/{0}/protocol/openid-connect/token";

            public static class V1
            {
                public const string V1Path = BaseReportiumPath + "v1/";
                public const string TestsResource = V1Path + "test-execution-management/";
                public const string StepsResource = TestsResource + "{0}/steps/"; // This string is formatted with the current test id
            }

            public static class QueryParameterNames
            {
                public const string TenantId = "TENANTID";
                public const string Tags = "tags";
                public const string ExternalId = "externalId";
            }
        }
    }
}
