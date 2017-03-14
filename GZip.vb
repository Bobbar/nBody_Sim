Imports System.IO
Imports System.IO.Compression
Module GZip

    Public Sub Compress(DataStream As Stream, fileToCompress As FileInfo)
        Using compressedFileStream As FileStream = File.Create(fileToCompress.FullName)
            Using compressionStream As New GZipStream(compressedFileStream, CompressionLevel.Fastest)
                DataStream.Position = 0
                DataStream.CopyTo(compressionStream)
            End Using
        End Using
    End Sub

    Public Function Decompress(ByVal fileToDecompress As FileInfo) As MemoryStream
        Using originalFileStream As FileStream = fileToDecompress.OpenRead()
            Dim currentFileName As String = fileToDecompress.FullName
            Dim newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length)
            Dim decompressedFileStream As New MemoryStream 'FileStream = File.Create(newFileName)
            Using decompressionStream As GZipStream = New GZipStream(originalFileStream, CompressionMode.Decompress)
                    decompressionStream.CopyTo(decompressedFileStream)
                    Console.WriteLine("Decompressed: {0}", fileToDecompress.Name)
                End Using
                Return decompressedFileStream
                ' End Using
            End Using
    End Function

    Public Sub OpenRendering(Filename As String)
        Dim DecompFile As New FileInfo(Filename)
        Dim fStream As MemoryStream = Decompress(DecompFile)
        fStream.Position = 0 ' reset stream pointer
        _nestedArrayForProtoBuf = ProtoBuf.Serializer.Deserialize(Of List(Of ProtobufArray(Of Body_Rec_Parms)))(fStream) 'bf.Deserialize(fStream) ' read from file
    End Sub
    Public Sub SaveRendering(Filename As String)
        Dim fStream As New MemoryStream
        ProtoBuf.Serializer.Serialize(fStream, _nestedArrayForProtoBuf)
        Dim CompFile As New FileInfo(Filename)
        Compress(fStream, CompFile)
    End Sub

End Module
