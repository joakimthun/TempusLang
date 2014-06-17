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

func printArguments(arg1:string, arg2:int, arg3:int)
{
	println arg1
	println arg2
	println arg3
}

func main()
{
	strField = 'Hello World!'
	printField()
	printInteger()
    printArguments('Hello world!', 10, 10 + 10 * 5)
}
```