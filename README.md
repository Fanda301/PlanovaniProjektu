V tomto repozitáři se nachází projekt a databáze s administrátorským účtem.
Pro zporovoznění doporučuji první na svůj SQL server obnovit zálohu databáze "db_projekty"
Dále si stáhnout celý zdrojový kód projektu, a ve složce "Models" ve třídě "DbProjektyContext" v metodě "OnConfiguring" změnit v connection stringu název serveru a přihlašovací údaje na server.
Pokud se vám podaří správně propojit projekt s databází, tak po spuštění se přihlásit s předzdívkou "admin" a heslem "12345". Poté si můžete založit svůj účet tento původní smazat.
