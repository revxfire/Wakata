Public Class vcliente
    Dim idcliente As Integer
    Dim nombre, apellido, direccion, telefono, dni As String

    'Setter y Getters

    Public Property gidcliente
        Get
            Return idcliente
        End Get
        Set(value)
            idcliente = value
        End Set
    End Property

    Public Property gnombre
        Get
            Return nombre
        End Get
        Set(value)
            nombre = value
        End Set
    End Property

    Public Property gapellido
        Get
            Return apellido
        End Get
        Set(value)
            apellido = value
        End Set
    End Property

    Public Property gdireccion
        Get
            Return direccion
        End Get
        Set(value)
            direccion = value
        End Set
    End Property

    Public Property gtelefono
        Get
            Return telefono

        End Get
        Set(value)
            telefono = value
        End Set
    End Property

    Public Property gdni
        Get
            Return dni

        End Get
        Set(value)
            dni = value
        End Set
    End Property

    'Constructores

    Public Sub New()

    End Sub

    Public Sub New(ByVal idcliente As Integer, ByVal nombre As String, ByVal apellido As String, ByVal direccion As String, ByVal Telefono As String, ByVal dni As String)
        gidcliente = idcliente
        gnombre = nombre
        gapellido = apellido
        gdireccion = direccion
        gtelefono = Telefono
        gdni = dni
    End Sub
End Class
