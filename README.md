# EmployeeManagement
Coursework project on ADO .Net / WinForms / WPF

This project was created as a term paper for the ADO .Net / WinForm / WPF course. The grade for the coursework is 11 out of 12.
It's a client-server application that allows a development team to manage project tasks. Simply put, a To-Do application.

The server is built using WinForms.
The client application is made using WPF.

## **User roles**
The client part is divided into 3 separate applications according to roles.

**User** - can only accept tasks or submit them.
![myTasks](https://github.com/zedicus25/EmployeeManagement/assets/95874337/12687e0f-3ca9-4847-a0a9-b1746c68811e)

**User+** - can do the same as the previous roles, and can also create tasks or assign an employee to a specific task.
![taskCreation](https://github.com/zedicus25/EmployeeManagement/assets/95874337/7d98fdf5-226b-4772-bc1c-5ac0848a24d4)

**Admin** -  can do the same as the previous roles, and has the ability to create new users, new projects, new roles, etc. That is, a user with this role has a CRUD architecture to every table in the database.
![allPanel](https://github.com/zedicus25/EmployeeManagement/assets/95874337/94c66d9c-b820-43bd-b47b-d53273ecb475)

## **Server**
The server is responsible for interacting with the database, and this is implemented using EF Core. The server also has a simple interface for starting the server, stopping it, and displaying the users who are currently connected.
![server](https://github.com/zedicus25/EmployeeManagement/assets/95874337/1b9bc98a-7b31-40a1-9601-2da063759c5e)

## **Client applications**
The interaction between the client and the server is implemented using a TCP connection. The client connects to the server using an IP address and port. To receive data, the application sends special commands to the server, and the server returns data from the database. Requests to the server are sent asynchronously, i.e. when requesting data, the application continues to work and does not stop until the data is received.
![allTasks](https://github.com/zedicus25/EmployeeManagement/assets/95874337/9a44a7de-5a4f-4996-b963-5c142683fa1a)

## **Database**
The database is created in accordance with the forms of normalisation.
![db](https://github.com/zedicus25/EmployeeManagement/assets/95874337/7cc36b87-e90c-4e0a-95d0-e66a4d47b76f)
