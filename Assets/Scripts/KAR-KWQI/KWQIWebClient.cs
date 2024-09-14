/*
0.3.0 implementation of how the KAR Workshop Quick Install format.

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

using System;
using System.IO;
using System.Net;
using System.Text;

//defines a common class for Web Client implementations for KWQI
class KWQIWebClient
{
    //check if the current version is the same as the latest git release
    static public bool CheckVersion_GitRelease(string author, string repo, string currentVersion)
    {
        string GitHub = "https://api.github.com/repos/" + author + "/" + repo + "/releases";
        string lazyTag = $"\"tag_name\": \"{currentVersion}\"";
        WebClient web = new WebClient();
        web.Headers["Content-Type"] = "application/json";
        web.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:87.0) Gecko/20100101 Firefox/87.0");
        web.Encoding = Encoding.UTF8;
        string incomingData = web.DownloadString(GitHub);

        return incomingData.Contains(lazyTag);
    }

    //downloads a Archive on Windows (async)

	//downloads a Archive on Windows (NOT ASYNC) || returns the Archive it downloaded
	static public FileInfo Download_Archive_Windows(DirectoryInfo outputFolder, string archiveURL, string archiveName)
	{
        FileInfo archive = new FileInfo($"{outputFolder.FullName}/{archiveName}.zip");

        //attempts a download
		using (WebClient client = new WebClient())
        {
            try
            {
                client.DownloadFile(archiveURL, archive.FullName);
                Console.WriteLine($"Download completed: {archive.FullName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return new FileInfo("");
            }
        }
		return archive;
	}

    //downloads a gekko code file on Windows (NOT ASYNC) || returns the Gekko Code file it downloaded
	static public FileInfo Download_GekkoCodes_Windows(DirectoryInfo outputFolder, string URL, string gekkoFileGameID)
	{
        FileInfo gekkoCodeFile = new FileInfo($"{outputFolder.FullName}/{gekkoFileGameID}.ini");

        //attempts a download
        using (WebClient client = new WebClient())
        {
            try
            {
                client.DownloadFile(URL, gekkoCodeFile.FullName);
                Console.WriteLine("File downloaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return new FileInfo("");
            }
        }

		return gekkoCodeFile;
	}
}