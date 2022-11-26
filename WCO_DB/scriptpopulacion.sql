Use WCODB

INSERT INTO Account
(nickname, email, name, lastName, birthdate, country, password,isAdmin)
VALUES ('admin1', 'admin1@gmail.com', 'Admin', 'Admin1', '16-03-2000', 'Costa Rica', '3b612c75a7b5048a435fb6ec81e52ff92d6d795a8b5a9c17070f6a63c97a53b2', 1),
		('admin2', 'admin2@gmail.com', 'Admin', 'Admin2', '12-08-2001', 'Costa Rica', '3b612c75a7b5048a435fb6ec81e52ff92d6d795a8b5a9c17070f6a63c97a53b2', 1),
		('jmendoza', 'jmendoza@gmail.com', 'Jose', 'Mendoza', '16-03-2000', 'Costa Rica', '1cd763f4482ed8c2f58fe7608542b975c0b158c81aae7aaade5d58b0164b4a37', 1),    /*jose123*/
		('CarlitosHD', 'carlitos@gmail.com', 'Carlitos', 'HD', '16-06-2000', 'Colombia', '1cd763f4482ed8c2f58fe7608542b975c0b158c81aae7aaade5d58b0164b4a37', 1);     



INSERT INTO Team
(tournamentId, name, type)
VALUES (NULL, 'Real Madrid', 'Local'),
		(NULL, 'Barcelona', 'Local'),
		(NULL, 'Chelsea', 'Local'),
		(NULL, 'Bayern Munich', 'Local')
		;

INSERT INTO Team
(tournamentId, name, type)
VALUES (NULL, 'Costa Rica', 'Selection'),
		(NULL, 'Argentina', 'Selection'),
		(NULL, 'Brasil', 'Selection'),
		(NULL, 'Belgica', 'Selection'),
		(NULL, 'Espa�a', 'Selection'),
		(NULL, 'Alemania', 'Selection'),
		(NULL, 'Francia', 'Selection'),
		(NULL, 'Portugal','Selection')
		;

INSERT INTO Player
(name, team_id)
VALUES ('Thibaut Courtois', 0), ('AndriyLunin', 0), ('Daniel Carvajal', 0), ('Eder Militao', 0), ('David Alaba', 0), ('Nacho', 0), ('Ferland Mendy', 0), ('Antonio Rudriger', 0), ('Luka Modric', 0), ('Toni Kross', 0), ('Federico Valverde', 0), ('Lucas Vazquez', 0), ('Eden Hazard', 0), ('Karin Benzema', 0), ('Marco Asensio', 0), ('Rodrygo Goes', 0), ('Mariano D�az', 0), ('Viniius Junior', 0), ('Dani Ceballos', 0), ('Eduardo Camavinga', 0), ('Marc-Andr� ter Stegen', 1), ('I�aki Pe�a', 1), ('H�ctor Beller�n', 1), ('Gerard Piqu�', 1), ('Ronald Ara�jo', 1), ('Andreas Christensen', 1), ('Marcos Alonso', 1), ('Jules Kound�', 1), ('Eric Garc�a', 1), ('Sergio Busquets', 1), ('Pedri', 1), ('Franck Kessie', 1), ('Frenkie de Jong', 1), ('Robert Lewandowski', 1), ('Ansu Fati', 1), ('Ferran Torres', 1), ('Memphis Depay', 1), ('Raphinha', 1), ('Ousmane Demb�l�', 1), ('Jordi Alba', 1), ('Kepa Arrizabalaga', 2), ('�douard Mendy', 2), ('Thiago Silva', 2), ('Trevoh Chalobah', 2), ('Ben Chilwell', 2), ('Reece James', 2), ('Kalidou Koulibaly', 2), ('C�sar Azplilicueta', 2), ('Marc Cucurella', 2), ('Jorginho', 2), ('N`Golo Kant�', 2), ('Mateo Kovacic', 2), ('Christian Pulisic', 2), ('Mason Mount', 2), ('Denis Zakaria', 2), ('Hakim Ziyech', 2), ('Kai Havertz', 2), ('Pierre-Emerick Aubameyang', 2), ('Raheem Sterling', 2), ('Armando Broja', 2), ('Manuel Neuer', 3), ('Sven Ulreich', 3), ('Dayot Upamecano', 3), ('Matthijs de Ligt', 3), ('Benjamin Pavard', 3), ('Alphonso Davies', 3), ('Bouna Sarr', 3), ('Lucas Hern�ndez', 3), ('Noussair Mazraoui', 3), ('Joshua Kimmich', 3), ('Leon Goretzka', 3), ('Marcel Sabitzer', 3), ('Ryan Gravenberch', 3), ('Serge Gnabry', 3), ('Leroy San�', 3), ('Kingsley Coman', 3), ('Eric Maxim Choupo-Moting', 3), ('Sadio Man�', 3), ('Thomas M�ller', 3), ('Jamal Musiala', 3), ('Esteban Alvarado', 4), ('Aaron Cruz', 4), ('Juan Pablo Vargas', 4), ('Bryan Oviedo', 4), ('Francisco Calvo', 4), ('Kendall Watson', 4), ('Carlos Martinez', 4), ('Celso Borges', 4), ('Bryan Ruiz', 4), ('Anthony Hern�ndez', 4), ('Douglas Lopez', 4), ('Aaron Suarez', 4), ('Gerson Torres', 4), ('Orlando Galo', 4), ('Roan Wilson', 4), ('Alvaro Zamora', 4), ('Brandon Aguilera', 4), ('Anthony Contreras', 4), ('Johan Venegas', 4), ('Joel Campbell', 4), ('Emiliano Mart�nez', 5), ('Franco Armani', 5), ('Cristian Romero', 5), ('Lionel Messi', 5), ('Lisandro Mart�nez', 5), ('�ngel Correa', 5), ('Rodrigo de Paul', 5), ('Juli�n �lvarez', 5), ('Guido Rodr�guez', 5), ('Joaqu�n Correa', 5), ('Giovani Lo Celso', 5), ('Mahuel Molina', 5), ('Enzo Fern�ndez', 5), ('Leandro Paredes', 5), ('Thiago Almada', 5), ('Gonzalo Montiel', 5), ('Facundo Medina', 5), ('�ngel Di Mar�a', 5), ('Nicol�s Tagliafico', 5), ('Papu G�mez', 5), ('Vinicius Junior', 6), ('Neymar', 6), ('Antony', 6), ('Marquinhos', 6), ('Rodrygo', 6), ('Fabinho', 6), ('�der Milit�o', 6), ('Casemiro', 6), ('Alisson', 6), ('Richarlison', 6), ('Raphinha', 6), ('Bruno Guimar�es', 6), ('Ederson', 6), ('Lucas Paquet�', 6), ('Bremer', 6), ('Roberto Firmino', 6), ('Matheus Cunha', 6), ('Renan Lodi', 6), ('Roger Iba�ez', 6), ('Fred', 6), ('Kevin De Bruyne', 7), ('Thibaut Courtois', 7), ('Youri Tielemans', 7), ('Yannick Carrasco', 7), ('Charles De Ketelaere', 7), ('Amadou Onana', 7), ('Leander Dendoncker', 7), ('Timothy Castagne', 7), ('Leandro Trossard', 7), ('Arthur Theate', 7), ('Alexis Saelemaekers', 7), ('Hans Vanaken', 7), ('Wout Faes', 7), ('Eden Hazard', 7), ('Thorgan Hazard', 7), ('Lo�s Openda', 7), ('Koen Casteels', 7), ('Michy Batshuayi', 7), ('Jason Denayer', 7), ('Matz Sels', 7), ('Pedri', 8), ('Rodri', 8), ('Gavi', 8), ('Pau Torres', 8), ('Marcos Llorente', 8), ('Carlos Soler', 8), ('Ferran Torres', 8), ('Y�remy Pino', 8), ('Jos� Gay�', 8), ('Marco Asensio', 8), ('�lvaro Morata', 8), ('Unai Sim�n', 8), ('Robert S�nchez', 8), ('Hugo Guillam�n', 8), ('Pablo Sarabia', 8), ('David Raya', 8), ('Nico Williams', 8), ('Koke', 8), ('Borja Iglesias', 8), ('Daniel Carvajal', 8), ('Joshua Kimmich', 9), ('Jamal Musiala', 9), ('Kai Havertz', 9), ('Serge Gnabry', 9), ('Leroy San�', 9), ('Antonio R�diger', 9), ('Niklas S�le', 9), ('Nico Schlotterbeck', 9), ('Marc-Andr� ter Stegen', 9), ('David Raum', 9), ('Ilkay G�ndogan', 9), ('Timo Werner', 9), ('Thomas M�ller', 9), ('Thilo Kehrer', 9), ('Robin Gosens', 9), ('Matthias Ginter', 9), ('Benjamin Henrichs', 9), ('Lukas Nmecha', 9), ('Maximilian Arnold', 9), ('Armel Bella-Kotchap', 9), ('Kylian Mbapp�', 10), ('Christopher Nkunku', 10), ('Aur�lien Tchouameni', 10), ('Jules Kound�', 10), ('Eduardo Camavinga', 10), ('Ousmane Demb�l�', 10), ('Rapha�l Varane', 10), ('Ferland Mendy', 10), ('Dayot Upamecano', 10), ('William Saliba', 10), ('Antoine Griezmann', 10), ('Benjamin Pavard', 10), ('Beno�t Badiashile', 10), ('Matt�o Guendouzi', 10), ('Randal Kolo Muani', 10), ('Youssouf Fofana', 10), ('Jordan Veretout', 10), ('Alban Lafont', 10), ('Jonathan Clauss', 10), ('Adrien Truffert', 10), ('Bruno Fernandes', 11), ('Bernardo Silva', 11), ('R�ben Dias', 11), ('Rafael Le�o', 11), ('Jo�o F�lix', 11), ('Jo�o Cancelo', 11), ('Diogo Jota', 11), ('Nuno Mendes', 11), ('Matheus Nunes', 11), ('R�ben Neves', 11), ('Vitinha', 11), ('Diogo Dalot', 11), ('Jo�o Palhinha', 11), ('Diogo Costa', 11), ('Cristiano Ronaldo', 11), ('Ricardo Horta', 11), ('Jos� S�', 11), ('William Carvalho', 11), ('Danilo Pereira', 11), ('Tiago Djal�', 11);

INSERT INTO Tournament
(to_id, name, startDate, endDate, description, type)
VALUES ('2e9af5', 'Champions League', '12-12-12', '13-13-13', 'Organizado X-FIFA', 'Local')
;

UPDATE [dbo].[Team]
SET [tournamentId] = '2e9af5'
WHERE [te_id] = 0 or [te_id] = 1 or [te_id] = 2 or [te_id] = 3;


INSERT INTO Bracket
(name, tournamentId)
VALUES ('Semifinales', '2e9af5'),
		('Final', '2e9af5')
;








