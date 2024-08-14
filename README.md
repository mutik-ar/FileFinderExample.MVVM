Цель1: реализовать пример на шаблоне MVVM

Цель2: Использовать асинхронное программирование для длительных задач по поиску файлов по шаблону.

Данный проект является портированием проекта "Пишем на C# программу для поиска файлов, используя ProgressBar и BackgroundWorker" https://allineed.ru/development/dotnet-development/charp-development/89-csharp-program-for-searching-files. Так же был использован проект https://github.com/daiplusplus/ShellFileDialogs, реализующий FolderBrowserDialogSelectSearchDirectory, так как данного функционала нет в .Net.

Проект, который я взял за основу, реализован на .Net Framework4.0, Windows Forms, BackgroundWorker. 

Моя задача была реализовать данный проект на современных технологиях и подходах.

Что получилось:

Платформа .Net6.0, WPF, архитектурный шаблон MVVM, ОПП, поведенческий шаблон Observer, принципы SOLID, асинхронная обработка длительных операций на основе Task, технология MEF (подключение внешних библиотек с реализациями интерфейсов IModel и IViewModel).

Компиляция:

1 вариант:

1 этап скопилировать проект Model (внешняя библиотека для FileFinderExample)

2 этап скопилировать проект ViewModel (внешняя библиотека для FileFinderExample)

3 этап скопилировать проект FileFinderExample

2 вариант:

Надо перекомпилировать (Rebuild) проект FileFinderExample. Срипт в проекте настроен так, чтобы перед компиляцией проекта FileFinderExample скопилируются проекты Model и  ViewModel


![image](https://github.com/user-attachments/assets/d64b296b-3c92-4047-bd8d-e751e0e8b12a)




