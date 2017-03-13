Imports System.IO
Imports System.IO.Compression
Module GZip

    Public Sub Compress(DataStream As Stream, fileToCompress As FileInfo) 'fileToCompress As FileInfo)
        'For Each fileToCompress As FileInfo In directorySelected.GetFiles()

        ' Using originalFileStream As FileStream = fileToCompress.OpenRead()
        If (File.GetAttributes(fileToCompress.FullName) And FileAttributes.Hidden) <> FileAttributes.Hidden And fileToCompress.Extension <> ".gz" Then
                Using compressedFileStream As FileStream = File.Create(fileToCompress.FullName & ".gz")

                    Using compressionStream As New GZipStream(compressedFileStream, CompressionMode.Compress)

                    DataStream.CopyTo(compressionStream)
                End Using
                End Using
                Dim info As New FileInfo(fileToCompress.DirectoryName & "\" & fileToCompress.Name & ".gz")
                Console.WriteLine("Compressed {0} from {1} to {2} bytes.", fileToCompress.Name,
                                      fileToCompress.Length.ToString(), info.Length.ToString())

            End If
        ' End Using
        '  Next
    End Sub




End Module
