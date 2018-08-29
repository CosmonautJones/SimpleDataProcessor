Imports System
Imports System.IO
Imports System.Text

Module Module1

    Sub Main()

        Dim DisplayMenu As Boolean = True
        While (DisplayMenu)
            DisplayMenu = MainMenu()
        End While

    End Sub

    Function MainMenu() As Boolean
        Console.Write("Enter filename here: ")
        Dim fileName As String = Console.ReadLine()

        If fileName = "Exit" Or fileName = "exit" Then
            Return False

        ElseIf Not File.Exists(fileName) Then
            Console.WriteLine("No such file exists! Please enter an existing file")
            Console.WriteLine("")
            Return True

        ElseIf File.Exists(fileName) Then
            DataProcessor(fileName)
            Console.WriteLine("Type Exit to quite program")
            Console.WriteLine("")
            Return True
        End If

    End Function

    Sub DataProcessor(fileName As String)

        Dim dic As SortedDictionary(Of String, Integer) = New SortedDictionary(Of String, Integer)
        Dim resultDic As SortedDictionary(Of String, Integer) = New SortedDictionary(Of String, Integer)

        Dim resultList As ArrayList = New ArrayList()
        Dim values() As String

        Dim val As String
        Dim key As String

        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(fileName)

        ' the data is read correctly by spliting from the spaces
        values = fileReader.Split(New String() {},
                 StringSplitOptions.RemoveEmptyEntries)

        For Each item As String In values
            If dic.ContainsKey(item) Then
                dic(item) += 1
            Else
                dic.Add(item, 1)
            End If
        Next

        For Each kvp As KeyValuePair(Of String, Integer) In dic
            If Integer.TryParse(kvp.Key, Nothing) Then
                val = Convert.ToInt32(kvp.Value)
                key = Convert.ToInt32(kvp.Key)
                If val = 1 Then
                    resultList.Add(key)
                Else
                    resultList.Add(key + ":" + val)
                End If
            End If
        Next

        resultList.Sort()


        Dim sendFile As String = fileName + ".ret"
        If Not File.Exists(sendFile) Then
            ' Create a file to write to.
            Using sw As StreamWriter = File.CreateText(sendFile)
                For Each item In resultList
                    sw.WriteLine(item)
                Next
            End Using
            Console.WriteLine("Success!")
            Console.WriteLine("")
        Else
            Using sw As StreamWriter = File.AppendText(sendFile)
                For Each item In resultList
                    sw.WriteLine(item)
                Next
                Console.WriteLine("Successfully wrote to file!")
                Console.WriteLine("")
            End Using
        End If

    End Sub


End Module