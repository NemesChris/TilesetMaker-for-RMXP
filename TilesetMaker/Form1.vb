Public Class Form1
    Dim alap, pluspic As Bitmap
    Dim newpic As Bitmap

    'ALAPKÉP BETÖLTÉSE
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            alap = Bitmap.FromFile(OpenFileDialog1.FileName)
            PictureBox1.Image = alap
        End If

    End Sub

    'TOLDALÉKKÉP BETÖLTÉSE
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click

        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            pluspic = Bitmap.FromFile(OpenFileDialog1.FileName)
            PictureBox2.Image = pluspic
        End If

    End Sub

    'ÖSSZERAKÁS
    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        'kell egy új kép a kettő méretének összegéből
        newpic = New Bitmap(PictureBox2.Width, PictureBox2.Height + PictureBox1.Height)

        Dim red, green, blue As Single
        Dim color As Color
        ProgressBar1.Maximum = PictureBox2.Height + PictureBox1.Height
        ProgressBar1.Value = 0

        'és ezt fel kell tölteni először az egyes kép pixeleiből
       
        'Minden sorra
        For row As Integer = 0 To alap.Height - 1
            'Minden oszlopra
            For Column As Integer = 0 To alap.Width - 1
                red = alap.GetPixel(Column, row).R
                green = alap.GetPixel(Column, row).G
                blue = alap.GetPixel(Column, row).B
                color = color.FromArgb(red, green, blue)
                newpic.SetPixel(Column, row, color)
            Next
            ProgressBar1.Value += 1
        Next

        'majd a pluszkép pixeleiből
        'Minden sorra
        For row As Integer = 0 To pluspic.Height - 1
            'Minden oszlopra
            For Column As Integer = 0 To pluspic.Width - 1
                red = pluspic.GetPixel(Column, row).R
                green = pluspic.GetPixel(Column, row).G
                blue = pluspic.GetPixel(Column, row).B
                color = color.FromArgb(red, green, blue)
                newpic.SetPixel(Column, PictureBox1.Height + row, color)
            Next
            ProgressBar1.Value += 1
        Next

        PictureBox1.Image = newpic
    End Sub

    Private Sub PictureBox1_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim fsave As New SaveFileDialog
            fsave.Filter = "*.png|*.png"
            If fsave.ShowDialog = Windows.Forms.DialogResult.OK Then
                PictureBox1.Image.Save(fsave.FileName, System.Drawing.Imaging.ImageFormat.Png)
                MsgBox("A kép kimentése sikeres!", vbInformation, "Kép kimentése")
            End If
        End If
    End Sub
End Class
