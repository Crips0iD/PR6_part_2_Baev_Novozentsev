#### ПРАКТИЧЕСКАЯ РАБОТА №6 СОЗДАНИЕ АВТОМАТИЗИРОВАННОГО UNIT-ТЕСТА Часть 2

Скриншот окна тестов:
![скриншот тестов](https://github.com/Crips0iD/PR6_part_2_Baev_Novozentsev/blob/master/Тесты%20скрин.png)

Скриншот из СУБД:
![скриншот из бд](https://github.com/Crips0iD/PR6_part_2_Baev_Novozentsev/blob/master/БД%20скрин.png)

#### Вывод о проведенном тестировании

- 1-ый unit-тест является "тестом" теста, чтобы на примере простого расчета показать работу теста.
- 2-ой unit-тест проверяет ошибки при входе с неверными данными.
- 3-ий unit-тест проверяет вход пользователя с верными данными.
- 4-ый unit-тест проверяет ошибки при попытке регистрации с некорректными данными.

Были ошибки в 2, 3 и 4 unit-тестах, т.к. программа жаловалась на неверную версию библиотек. Исправлено было путем подбора правильной совместимости библиотек в проекте.

Все тесты прошли успешно.
