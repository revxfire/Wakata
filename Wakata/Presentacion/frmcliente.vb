Public Class frmcliente

    Private dt As New DataTable

    Private Sub frmcliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mostrar()
    End Sub

    Private Function limpiar()
        btnGuardar.Visible = True
        btnEditar.Visible = False
        txtNombre.ResetText()
        txtApellido.ResetText()
        txtDireccion.ResetText()
        txtTelefono.ResetText()
        txtDNI.ResetText()
        txtIdCliente.ResetText()

    End Function
    Private Function mostrar()
        Try
            Dim func As New Fcliente
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

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtIdCliente.TextChanged

    End Sub

    Private Sub txtNombre_TextChanged(sender As Object, e As EventArgs) Handles txtNombre.TextChanged

    End Sub

    Private Sub txtNombre_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtNombre.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.erroricono.SetError(sender, "")
        Else
            Me.erroricono.SetError(sender, "Ingrese el nombre del cliente por favor")
        End If
    End Sub

    Private Sub txtApellido_TextChanged(sender As Object, e As EventArgs) Handles txtApellido.TextChanged

    End Sub

    Private Sub txtApellido_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtApellido.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.erroricono.SetError(sender, "")
        Else
            Me.erroricono.SetError(sender, "Ingrese el apellido del cliente por favor")
        End If
    End Sub

    Private Sub txtDireccion_TextChanged(sender As Object, e As EventArgs) Handles txtDireccion.TextChanged

    End Sub

    Private Sub txtDireccion_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtDireccion.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.erroricono.SetError(sender, "")
        Else
            Me.erroricono.SetError(sender, "Ingrese la direccion del cliente por favor")
        End If
    End Sub

    Private Sub txtDNI_TextChanged(sender As Object, e As EventArgs) Handles txtDNI.TextChanged

    End Sub

    Private Sub txtDNI_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtDNI.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.erroricono.SetError(sender, "")
        Else
            Me.erroricono.SetError(sender, "Ingrese el dni del cliente por favor")
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        limpiar()
        mostrar()

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Me.ValidateChildren = True And txtNombre.Text <> "" And txtApellido.Text <> "" And txtDireccion.Text <> "" And txtApellido.Text <> "" And txtTelefono.Text <> "" And txtDNI.Text <> "" Then
            Try
                Dim dts As New vcliente
                Dim func As New Fcliente
                dts.gnombre = txtNombre.Text
                dts.gapellido = txtApellido.Text
                dts.gdireccion = txtDireccion.Text
                dts.gtelefono = txtTelefono.Text
                dts.gdni = txtDNI.Text

                If func.insertar(dts) Then
                    MessageBox.Show("Cliente registrado correctamente", "Guardando Datos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mostrar()
                    limpiar()
                Else
                    MessageBox.Show("Cliente no fue registrado intente de nuevo", "Guardando Datos", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        txtIdCliente.Text = dataListado.SelectedCells.Item(1).Value
        txtNombre.Text = dataListado.SelectedCells.Item(2).Value
        txtApellido.Text = dataListado.SelectedCells.Item(3).Value
        txtDireccion.Text = dataListado.SelectedCells.Item(4).Value
        txtTelefono.Text = dataListado.SelectedCells.Item(5).Value
        txtDNI.Text = dataListado.SelectedCells.Item(6).Value
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

        result = MessageBox.Show("Realmente desea modificar los datos del cliente?", "Modificando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

        If result = Windows.Forms.DialogResult.OK Then

            If Me.ValidateChildren = True And txtNombre.Text <> "" And txtApellido.Text <> "" And txtDireccion.Text <> "" And txtApellido.Text <> "" And txtTelefono.Text <> "" And txtDNI.Text <> "" And txtIdCliente.Text <> "" Then
                Try
                    Dim dts As New vcliente
                    Dim func As New Fcliente

                    dts.gidcliente = txtIdCliente.Text
                    dts.gnombre = txtNombre.Text
                    dts.gapellido = txtApellido.Text
                    dts.gdireccion = txtDireccion.Text
                    dts.gtelefono = txtTelefono.Text
                    dts.gdni = txtDNI.Text

                    If func.editar(dts) Then
                        MessageBox.Show("Cliente actualizado correctamente", "Guardando Datos", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        result = MessageBox.Show("Realmente desea elminar los clientes selecciondos", "Eliminar clientes", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.OK Then
            Try
                For Each row As DataGridViewRow In dataListado.Rows
                    Dim marcado As Boolean = Convert.ToBoolean(row.Cells("eliminar").Value)
                    If marcado Then
                        Dim onekey As Integer = Convert.ToInt32(row.Cells("idcliente").Value)
                        Dim vdb As New vcliente
                        Dim func As New Fcliente
                        vdb.gidcliente = onekey

                        If func.eliminar(vdb) Then
                        Else
                            MessageBox.Show("Cliente no fue eliminado", "Eliminar clientes", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    End If

                Next
                Call mostrar()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Cancelando eliminacion de registros", "Eliminar clientes", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call mostrar()
        End If
        Call limpiar()
    End Sub
End Class