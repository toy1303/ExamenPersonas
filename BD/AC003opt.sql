declare
@NumPersona int =1

while (@NumPersona<=10)
BEGIN
INSERT INTO Persona (Nombre, Edad, Email)
    VALUES (
        'Persona ' + CAST(@NumPersona AS NVARCHAR(MAX)),
        18 + @NumPersona,
        'Persona' + CAST(@NumPersona AS NVARCHAR(MAX)) + '@gmail.com'
    );
	set @NumPersona=@NumPersona+1
END