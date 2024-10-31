

create table Persona
(Id int identity(1,1) primary key,
Nombre nvarchar(max),
Edad int,
Email nvarchar(max)
);
GO

select * from Persona