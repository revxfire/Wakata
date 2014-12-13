Public Class frmcategoria
    Private dt As New DataTable


    Private Sub frmcliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mostrar()
    End Sub

    Private Function limpiar()
        btnGuardar.Visible = True
        btnEditar.Visible = False
        txtNombre.ResetText()
        txtIdCategoria.ResetText()

    End Function
    Private Function mostrar()
        Try
            Dim func As New fcategoria
            dt = func.mostrar
            dataListado.Columns.Item("Eliminar").Visible = False

            If dt.Rows.Count <> 0 Then
                dataListado.DataSource = dt
                txtBuscar.Enabled = True
                dataListado.ColumnHeadersVisible = True
                lblInexistente.Visible = False
            Else
                dataListado.DataSource = Nothing
                txtBuscar.Enabled = False
                dataListado.ColumnHeadersVisible = False
                lblInexistente.Visible = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        btnNuevo.Visible = True
        btnEditar.Visible = True

        Buscar()

    End Function



    Private Function buscar()
        Try
            Dim ds As New DataSet
            ds.Tables.Add(dt.Copy)
            Dim dv As New DataView(ds.Tables(0))

            dv.RowFilter = cboCampo.Text & " like '" & txtBuscar.Text & "%'"

            If dv.Count <> 0 Then
                lblInexistente.Visible = False
                dataListado.DataSource = dv
                ocultar_columnas()
            Else
                lblInexistente.Visible = True
                dataListado.DataSource = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Private Sub ocultar_columnas()
        dataListado.Columns(1).Visible = False
    End Sub

    Private Sub txtNombre_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtNombre.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.erroricono.SetError(sender, "")
        Else
            Me.erroricono.SetError(sender, "Ingrese el nombre de la categoria por favor")
        End If
    End Sub



    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        limpiar()
        mostrar()

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Me.ValidateChildren = True And txtNombre.Text <> "" Then
            Try
                Dim dts As New vcategoria
                Dim func As New fcategoria
                dts.gnombre_categoria = txtNombre.Text
                
                If func.insertar(dts) Then
                    MessageBox.Show("Categoria registrada correctamente", "Guardando Datos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mostrar()
                    limpiar()
                Else
                    MessageBox.Show("Categoria no fue registrado intente de nuevo", "Guardando Datos", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    mostrar()
                    limpiar()
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Falta Ingresar algunos datos", "Guardando Datos", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub dataListado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataListado.CellClick
        txtIdCategoria.Text = dataListado.SelectedCells.Item(1).Value
        txtNombre.Text = dataListado.SelectedCells.Item(2).Value
        btnEditar.Visible = True
        btnGuardar.Visible = False
    End Sub

    Private Sub dataListado_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataListado.CellContentClick
        If e.ColumnIndex = Me.dataListado.Columns.Item("eliminar").Index Then
            Dim chkcell As DataGridViewCheckBoxCell = Me.dataListado.Rows(e.RowIndex).Cells("Eliminar")
            chkcell.Value = Not chkcell.Value
        End If
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        Dim result As DialogResult

        result = MessageBox.Show("Realmente desea modificar los datos de la categoria?", "Modificando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

        If result = Windows.Forms.DialogResult.OK Then

            If Me.ValidateChildren = True And txtNombre.Text <> "" And txtIdCategoria.Text <> "" Then
                Try
                    Dim dts As New vcategoria
                    Dim func As New fcategoria

                    dts.gidcategoria = txtIdCategoria.Text
                    dts.gnombre_categoria = txtNombre.Text


                    If func.editar(dts) Then
                        MessageBox.Show("Categoria actualizado correctamente", "Guardando Datos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        mostrar()
                        limpiar()
                    Else
                        MessageBox.Show("Datos no modificados", "Guardando Datos", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        mostrar()
                        limpiar()
                    End If

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Else
                MessageBox.Show("Falta Ingresar algunos datos", "Guardando Datos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

    End Sub

    Private Sub cbEliminar_CheckedChanged(sender As Object, e As EventArgs) Handles cbEliminar.CheckedChanged
        If cbEliminar.CheckState = CheckState.Checked Then
            dataListado.Columns.Item("eliminar").Visible = True
        Else
            dataListado.Columns.Item("eliminar").Visible = False
        End If
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim result As DialogResult
        result = MessageBox.Show("Realmente desea elminar las categorias selecciondos", "Eliminar categorias", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.OK Then
            Try
                For Each row As DataGridViewRow In dataListado.Rows
                    Dim marcado As Boolean = Convert.ToBoolean(row.Cells("eliminar").Value)
                    If marcado Then
                        Dim onekey As Integer = Convert.ToInt32(row.Cells("idcategoria").Value)
                        Dim vdb As New vcategoria
                        Dim func As New fcategoria
                        vdb.gidcategoria = onekey

                        If func.eliminar(vdb) Then
                        Else
                            MessageBox.Show("Categorias no fue eliminado", "Eliminar categoria", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    End If

                Next
                Call mostrar()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Cancelando eliminacion de registros", "Eliminar categorias", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call mostrar()
        End If
        Call limpiar()
    End Sub
End Class