# Shroooms recognition

## Cel projektu
Celem naszego projektu jest stworzenie aplikacji, która pomoże niedoświadczonym grzybiarzom w rozpoznaniu grzyba. Chcemy w ten sposób zmniejszyć ilość wypadków spowodowanych zjedzeniem trującego grzyba.

## Architektura rozwiązania
![diagram](https://user-images.githubusercontent.com/73696833/204140300-11509b55-48ce-41d8-8fc5-bc95b20af218.png)

## Aplikacja webowa
Nasza aplikacja webowa opublikowana w Azure App Service została napisana w technologii Blazor Server z wykorzystaniem .NET 6. Aplikacja zawiera dwa widoki: widok główny służący do wgrania zdjęcia grzyba, które zostanie poddane klasyfikacji, oraz widok z rezultatem klasyfikacji. Użytkownik może wgrać tylko pliki z obrazem o rozszerzeniu .jpg i .png.

Ekran główny prezentuje się następująco:
![główny ekran](https://user-images.githubusercontent.com/73691017/204139527-ed3092fd-5235-495b-b395-f756540e684a.png)

Po wgraniu zdjęcia grzyba aplikacja po kilku sekundach prezentuje wynik:
![borowik](https://user-images.githubusercontent.com/73691017/204139547-0df7694f-0a55-4a3f-ac82-c8dab1b3a86b.png)

Aby wrócić ponownie do ekranu głównego wystarczy kliknąć na napis w headerze "Czy twój grzyb jest jadalny?".

Jeśli uzyskane prawdopodobieństwo jest niższe niż 70% aplikacja zwraca opis z informacją o tym, że aplikacji nie udało się rozpoznać grzyba oraz z prośbą o wgranie zdjęcia, na którym grzyb jest lepiej widoczny. Wynik poniżej 70% jest wynikiem niezadowalającym, dlatego nie chcemy wprowadzać użytkowników w błąd. Dla bardziej rozbudowanego modelu z większą bazą zdjęć grzybów prawdopodobnie ustalilibyśmy ten limit na około 90%.

Ekran po nieudanej próbie klasyfikacji:

![nieudane](https://user-images.githubusercontent.com/73691017/204139714-414edb72-8856-4fc6-a056-002ff900419c.png)

## Custom Vision
Aplikacja korzysta z klasyfikacji zdjęć za pomocą Custom Vision.
Do trenowania modelu użyliśmy dataset, który można znaleźć [tutaj](https://www.kaggle.com/datasets/derekkunowilliams/mushrooms).
By ograniczyć koszty związane z trenowaniem modelu nie wykorzystaliśmy wszystkich zdjęć. Wybraliśmy po kilkanaście rodzajów z kategorii jadalne, trujące i śmiertelne. W sumie wybraliśmy 52 gatunki.

## Blob Storage
Zdjęcia które osiągneły powyżej 70% prawdopodobieństwa, zostają wysłane i przechowywane w naszym Storage Account, w kontenerze z blobami. Nazwa zdjęcia składa się z rodzaju grzyba + unikalny identyfikator + rozszerzenie, dzięki czemu grzyby, będzie można wykorzystać w kolejnych iteracjach lub innych klasyfikacjach. 

## KeyVault
Wszystkie klucze i connection stringi, są przechowywane w Azure Key Vaulcie za pomocą secretów. Z KeyVaultem łączymy się za pomocą stworzonego w app service, managed identity, które jest częścią Azure AD. W KeyVaulcie jest dodane policy, które pozwala naszemu App Service mieć dostęp do secretów.

## Demo
https://www.youtube.com/watch?v=zGd1doMS_b8

## Zespół
* Oskar Reszka
* Konrad Trusiewicz https://github.com/Femtoo
* Zuzanna Gaik
