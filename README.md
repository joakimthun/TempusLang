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

func printInteger()
{
    var result = 10 * 5 + 5 / 6 * 66
    println result
}

func main()
{
	strField = "Hello World!"
	printField()
}
```