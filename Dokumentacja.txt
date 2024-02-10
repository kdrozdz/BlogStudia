Projekt powstał w ASP.NET

# Przeczytaj!
Przed odpaleniem projektu należy zrobić update-database za pomocą Tools > NuGet Package Manager > Package Manager Console


Logowanie/rejestracja/wylogowywanie odbywa sie przy pomocy modelu ApplicationUser rozszerzającej bazowe Identity

Wszystkie pola są walidowane.

## Konfiguracja E-Mail
Aby można było wysyłać e-mail z potwierdzeniem rejestracji trzeba skonfigurować klienta SMTP:
1. Areas -> Identity -> Pages -> Account -> Register.cshtml.cs -> w funkcji SendEmailAsync umieszczamy realne dane serwera SMTP (Gmail etc.)
2. Wchodzimy w Program.cs i w linijcie 21 zmieniamy
   ```
   options.SignIn.RequireConfirmedAccount = false
   ```
   na
   ```
   options.SignIn.RequireConfirmedAccount = true
   ```
I powinno działać.

### Informacje dodatkowe
Stopnie dostępu:

Niezalogowany uzytkownik - widzi tyko posty na stronie glównej.
Zalogowany - to samo co nie zalogowany, moze dodawać posty oraz je wyszukiwać.
Admin - Moze dodawać, wyszukiwać oraz ma dodatkowy panel do edycji/usuwania postów, widzi liste wszystkich uzytkownikow

Posty - Skladaja sie z Title, content, tag, author, createdAt i AuthorId
Uzytkownik wypełnia tylko title, content oraz tag, reszta jest uzupełniana automatycznie.

Tag jest zahardkodowany w kodzie, jest elementem obiektu post, jest to enum z ktorego mozemy wybrać wartość.
Możemy wyszukiwać po tagach, jeżeli jakiekolwiek posty istnieję w bazie.
