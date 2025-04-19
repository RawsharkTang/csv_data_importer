## CSVResource C#
A plugin inspired by [godot-csv-typed-importer](https://github.com/citizenll/godot-csv-typed-importer.git) 
Basically a wrapper for [sep](https://github.com/nietras/Sep.git) csv parser.  
Import csv table and access the data with C# and GDscript code! 

### How to use
![alt text](image.png)
- Header (1st row) is needed
- Support Comma or tab Separator, or you can use Auto and leave it the sep to decide.
  - Check [sep](https://github.com/nietras/Sep.git) to see more detail
- 2rd row can be set as type indicator.
  - Current Supported Type: int/float/string/bool/json
    - json type use the godot str_to_var native calls
- Skip row can be set to skip a second header row.

```
id,name,description,level,percentage,switch,sequence
string,string,string,int,float,bool,json
id,Name,description,Rank,Hit Rate,can_hit,Damage
foo_0,Foo,Test data for csv importer,1,0.1,True,"[1,2,3]"
```

| id     | name   | description                | level | percentage | boolean | sequence |
| ------ | ------ | -------------------------- | ----- | ---------- | ------- | -------- |
| string | string | string                     | int   | float      | bool    | json     |
| Id     | Name   | Description                | Level | Percentage | Boolean | Sequence |
| foo_0  | Foo    | Test data for csv importer | 1     | 0.1        | TRUE    | [1,2,3]  |


