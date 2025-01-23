Feature: Ingreso

A short summary of the feature

@tag1
Scenario: Ingreso de Cliente
	Given llenar los campos de la BDD
	 | Cedula      | Apellidos  | Nombres    | FechaNacimiento | Mail               | Telefono   | Direccion  | Provincia  | Saldo  | Estado |
     | 1234567890  | Perez      | Juan       | 01/01/1990      | juanperez@test.com | 0987654321 | Quito      | Pichincha  | 100.00 | true   |
	When El registro se ingresa en la BDD
	 | Cedula      | Apellidos  | Nombres    | FechaNacimiento | Mail               | Telefono   | Direccion  | Provincia  | Saldo  | Estado |
     | 1234567890  | Perez      | Juan       | 01/01/1990      | juanperez@test.com | 0987654321 | Quito      | Pichincha  | 100.00 | true   |
	Then El resultado se me ingresa en la BDD
	| Cedula      | Apellidos  | Nombres    | FechaNacimiento | Mail               | Telefono   | Direccion  | Provincia  | Saldo  | Estado |
    | 1234567890  | Perez      | Juan       | 01/01/1990      | juanperez@test.com | 0987654321 | Quito      | Pichincha  | 100.00 | true   |
