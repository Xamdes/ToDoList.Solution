# Epicodus Week 6 Day 3 To Do List

## Author(s)

  * Steven Colburn
  * Will Kinzig

## Date

06/27/2018

## Specifications

  * Connects to MySql database
  * adds items to database
  * retrieves items from database
  * clears all items from database
    * optionally resets auto-increment
  * List all items on front end webpage

## Database

  * CREATE TABLE `sc_todolist`.`items` ( `id` INT NOT NULL AUTO_INCREMENT , `description` VARCHAR(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL , `date` DATE NULL , `category_id` INT NULL , PRIMARY KEY (`id`)) ENGINE = InnoDB;

  * CREATE TABLE `sc_todolist`.`categories` ( `id` INT NOT NULL AUTO_INCREMENT , `category` VARCHAR(255) NULL , PRIMARY KEY (`id`)) ENGINE = InnoDB;

## NuGet Config

<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
  </packageSources>
</configuration>

## License

MIT License

Copyright (c) 2018 Steven Colburn

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
