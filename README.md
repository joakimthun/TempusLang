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

func main()
{
	strField = "Hello World!"
	printField()
}
```