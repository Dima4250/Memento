using System;
using System.Collections.Generic;

// Originator - создатель
class Editor
{
    public string Content { get; set; } // содержимое экземпляра

    //Создание экземпляра класса, который сохраняет его текущее состояние
    public EditorMemento CreateMemento() => new EditorMemento(Content);

    // Восстанавливает состояние
    public void RestoreMemento(EditorMemento memento) => Content = memento.SavedContent;

    // Вывод содержимого
    public void ShowContent() => Console.WriteLine($"Текущий текст: {Content}");
}

// Memento - сохранение состояния
class EditorMemento
{
    public string SavedContent { get; } // Сохраненное содержимое
    // Конструктор принимает и сохраняет состояние
    public EditorMemento(string content) => SavedContent = content;
}

// Caretaker - опекун, откат изменений 
class History
{
    // Стек для хранения
    private Stack<EditorMemento> _mementos = new Stack<EditorMemento>();

    //Сохранение
    public void SaveState(Editor editor) => _mementos.Push(editor.CreateMemento());

    // Откат изменений
    public void Undo(Editor editor)
    {
        if (_mementos.Count > 0)
            editor.RestoreMemento(_mementos.Pop());
    }
}

class Program
{
    static void Main()
    {
        var editor = new Editor();  // Создание экземпляров классов
        var history = new History();//

        editor.Content = "Первая версия текста"; // Сохранение первого сос-я
        history.SaveState(editor);
        editor.ShowContent();

        history.SaveState(editor); // Сохранение первого сос-я, перед его изменением
        editor.Content = "Измененный текст";
        editor.ShowContent();

        history.Undo(editor); // Откат
        editor.ShowContent();
    }
}