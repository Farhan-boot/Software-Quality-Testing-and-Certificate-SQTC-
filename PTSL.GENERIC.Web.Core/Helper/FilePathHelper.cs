namespace PTSL.GENERIC.Web.Core.Helper;

public static class FilePathHelper
{
    public static string GetFileNameWithExtensionFromUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return string.Empty;
        }

        string fileNameWithQuery = url[(url.LastIndexOf('/') + 1)..];

        int queryIndex = fileNameWithQuery.IndexOf('?');
        if (queryIndex != -1)
        {
            fileNameWithQuery = fileNameWithQuery[..queryIndex];
        }

        string fileNameWithExtension = Path.GetFileName(fileNameWithQuery);

        return fileNameWithExtension;
    }
}
