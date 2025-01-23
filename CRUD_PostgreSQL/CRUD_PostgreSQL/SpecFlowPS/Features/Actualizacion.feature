Feature: Actualización

Scenario: Actualización de Cliente Existente
    Given el cliente existe en la base de datos
        | Cedula     | Apellidos | Nombres | FechaNacimiento | Mail               | Telefono   | Direccion | Provincia | Saldo  | Estado |
        | 1234567890 | Perez     | Juan    | 01/01/1990      | juanperez@test.com | 0987654321 | Quito     | Pichincha | 100.00 | true   |
    When actualizo los datos del cliente
        | Cedula     | Apellidos | Nombres | FechaNacimiento | Mail                 | Telefono   | Direccion     | Provincia | Saldo  | Estado |
        | 1234567890 | Perez     | Juan    | 01/01/1990      | juan.perez@gmail.com | 0987654322 | Quito Central | Pichincha | 200.00 | true   |
    Then el cliente debe estar actualizado en la base de datos
        | Cedula     | Apellidos | Nombres | FechaNacimiento | Mail                 | Telefono   | Direccion     | Provincia | Saldo  | Estado |
        | 1234567890 | Perez     | Juan    | 01/01/1990      | juan.perez@gmail.com | 0987654322 | Quito Central | Pichincha | 200.00 | true   |