### Variables
- Variables names must be in lowerCamelCase
- Abbreviations in names should be written in uppercase
- During initialization, it is possible not to transfer curly brackets and initializer members to a new line
```
public void Main()
{
	List<string> stringsList = new List<string>();
	string exportHTMLSource;
	
	// so it is possible
	var husband = new Person()
	{
		Age = 40,
		Height = 180
	};
	
	// so too
	var wife = new Person() { Age = 35, Height = 163 };
}
```

### Enums
- Enums names must be in uppercase and in snake_case
- It is possible not to transfer curly brackets and initializer members to a new line

```
// it is possible
public enum SOME_ENUM { A, B, C };

// so too
public enum SOME_ENUM 
{ 
	A, 
	B, 
	C 
};
```

### Classes
- Classes names must be in UpperCamelCase
- Fields names must be in lowerCamelCase
- Properties names must be in UpperCamelCase
- Methods names must be in UpperCamelCase
- If access level - private, Do NOT explicitly specify this
- For properties: between the opening bracket and the method, also after `;` put spaces
- If the body of the method is empty, it is NOT necessary to move the brackets to a new line, between the brackets put spaces
- Interfaces begin with the letter I, and the name in UpperCamelCase

```
class MyClass
{
	string userName;
	public int Id { get; set; }
	protected void MethodName() { }
}

interface IMyInterface
{
}
```
- With inheritance before and after `:` put spaces
- In case of multiple inheritance, between interface names put spaces
```
class MyClass : IMyInterface1, IMyInterface2
{
}
```

### Arrays
- There is NO space between the data type and the opening square bracket
- In the list of dimensions after each comma put space 
```
int[] arr = new int[10];
int[,,] arr = new int[10, 20, 30];
```

- Before and after operators: arithmetic, logical, comparisons put spaces 
```
int a = b + c;
int a = b || c;
if (a <= b)
```

### Functions: 
- There is no space between the function name and the opening bracket
- There is no space between the bracket and the parameter list
- There is no space between the brackets with an empty parameter list
- In the list of parameters after each comma put space

```
void Function1()
{
	Function2(1, 2);
}

int Function2(int x, int y)
{
	return x + y;
}
```

### for, if, while
- for `for` after `;` put space

```
for (int i = 0; i < n; i++)
{
	// some code
}

if (condition) 
{
	// some code
}
else
{
	// some code
}

while (condition)
{
	// some code
}
```
### switch
```
switch (condition)
{
	case 1:
	case 2:
	{
		// some code
	}
	default:
	{
		// some code
	}
} 
```

### Comments
- before and after `//` put spaces
```
SomeMethod(); // comment
```
