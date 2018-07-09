#!/usr/bin/env bash
cd ..
dotnet restore ./ToDoList
dotnet build ./ToDoList
dotnet restore ./ToDoList.Tests/
