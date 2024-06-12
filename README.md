Цель1: реализовать пример на шаблоне MVVM

Цель2: Использовать асинхронное программирование для длительных задач по поиску файлов по шаблону.

Данный проект является портированием проекта "Пишем на C# программу для поиска файлов, используя ProgressBar и BackgroundWorker" https://allineed.ru/development/dotnet-development/charp-development/89-csharp-program-for-searching-files. Так же был использован проект https://github.com/daiplusplus/ShellFileDialogs, реализующий FolderBrowserDialogSelectSearchDirectory, так как данного функционала нет в .Net.

Проект, который я взял за основу, реализован на .Net Framework4.0, Windows Forms, BackgroundWorker. 

Моя задача была реализовать данный проект на современных технологиях и подходах.

Что получилось:

Платформа .Net6.0, WPF, архитектурный шаблон MVVM, ОПП, поведенческий шаблон Observer, принципы SOLID, асинхронная обработка длительных операций на основе Task, MEF.

Компиляция:

1 этап скопилировать проект Model (внешняя библиотека для FileFinderExample)

2 этап скопилировать проект ViewModel (внешняя библиотека для FileFinderExample)

3 этап скопилировать проект FileFinderExample


![image](https://github.com/mutik-ar/FileFinderExample.MVVM/assets/90051513/b1cd67a6-a9e7-4a58-a681-de8f497947b5)

