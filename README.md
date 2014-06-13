Example program:
```
global strField : string

func printField()
{
	loop(10)
	{
		println strField
	}
}

func mainFunc() : main
{
	strField = "Hello World!"
	printField()
}
```