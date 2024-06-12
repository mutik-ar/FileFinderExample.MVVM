Цель1: реализовать пример на шаблоне MVVM

Цель2: Использовать асинхронное программирование для длительных задач по поиску файлов по шаблону.

Данный проект является портированием проекта "Пишем на C# программу для поиска файлов, используя ProgressBar и BackgroundWorker" https://allineed.ru/development/dotnet-development/charp-development/89-csharp-program-for-searching-files. Так же был использован проект https://github.com/daiplusplus/ShellFileDialogs, реализующий FolderBrowserDialogSelectSearchDirectory, так как данного функционала нет в .Net.

Проект, который я взял за основу, реализован на .Net Framework4.0, Windows Forms, BackgroundWorker. 

Моя задача была реализовать данный проект на современных технологиях и подходах.

Что получилось:

Платформа .Net6.0, WPF, архитектурный шаблон MVVM, принципы SOLID, асинхронная обработка длительных операций на основе Task.

![image](https://github.com/mutik-ar/FileFinderExample.MVVM/assets/90051513/aad565b6-becf-421a-bcaf-2d931b61a46f)
