Imports System.IO
Imports System.Collections.Generic
Imports iTextSharp.text
Imports iTextSharp.text.pdf


Public Class PdfMerge
    Private m_baseFont As BaseFont
    Private ReadOnly m_documents As List(Of PdfReader)
    Private totalPages As Integer


    Public Property BaseFont() As BaseFont
        Get
            Return m_baseFont
        End Get
        Set(ByVal value As BaseFont)
            m_baseFont = value
        End Set
    End Property


    Public ReadOnly Property Documents() As List(Of PdfReader)
        Get
            Return m_documents
        End Get
    End Property


    Public Sub AddDocument(ByVal filename As String)
        m_documents.Add(New PdfReader(filename))
    End Sub


    Public Sub AddDocument(ByVal pdfStream As Stream)
        m_documents.Add(New PdfReader(pdfStream))
    End Sub


    Public Sub AddDocument(ByVal pdfContents As Byte())
        m_documents.Add(New PdfReader(pdfContents))
    End Sub


    Public Sub AddDocument(ByVal pdfDocument As PdfReader)
        m_documents.Add(pdfDocument)
    End Sub


    Public Sub Merge(ByVal outputFilename As String)
        Merge(New FileStream(outputFilename, FileMode.Create))
    End Sub


    Public Sub Merge(ByVal outputStream As Stream)

        If outputStream Is Nothing OrElse Not outputStream.CanWrite Then
            Throw New Exception("OutputStream es nulo o no se puede escribir en éste.")
        End If
        Dim newDocument As Document = Nothing

        Try
            newDocument = New Document()
            Dim pdfWriter__1 As PdfWriter = PdfWriter.GetInstance(newDocument, outputStream)
            newDocument.Open()
            Dim pdfContentByte__2 As PdfContentByte = pdfWriter__1.DirectContent
            Dim currentPage As Integer = 1

            For Each pdfReader As PdfReader In m_documents
                For page As Integer = 1 To pdfReader.NumberOfPages
                    newDocument.NewPage()
                    Dim importedPage As PdfImportedPage = pdfWriter__1.GetImportedPage(pdfReader, page)
                    pdfContentByte__2.AddTemplate(importedPage, 0, 0)
                Next
            Next

        Finally
            outputStream.Flush()
            If newDocument IsNot Nothing Then
                newDocument.Close()
            End If
            outputStream.Close()
        End Try

    End Sub


    Public Sub New()
        m_documents = New List(Of PdfReader)()
    End Sub

End Class
