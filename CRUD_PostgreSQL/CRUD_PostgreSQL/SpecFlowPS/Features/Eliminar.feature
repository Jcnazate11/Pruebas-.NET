Feature: Eliminar

A short summary of the feature

@tag1
Scenario: Eliminar el Cliente
	Given Seleccionar el cliente para eliminar de la BDD
        | Cedula     | Apellidos | Nombres | FechaNacimiento | Mail                 | Telefono   | Direccion     | Provincia | Saldo  | Estado |
        | 1234567890 | Perez     | Juan    | 01/01/1990      | juan.perez@gmail.com | 0987654322 | Quito Central | Pichincha | 200.00 | true   |
	When Eliminamos el cliente de la BDD
	Then Verificamos que el cliente fue eliminado de la BDD
