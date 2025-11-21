# Gilded Rose starting position in C# xUnit

## Build the project

Use your normal build tools to build the projects in Debug mode.
For example, you can use the `dotnet` command line tool:

``` cmd
dotnet build GildedRose.sln -c Debug
```

## Run the Gilded Rose Command-Line program

For e.g. 10 days:

``` cmd
GildedRose/bin/Debug/net8.0/GildedRose 10
```

## Run all the unit tests

``` cmd
dotnet test
```

## TCR :)

```zsh
dotnet build && (dotnet test --no-build && git commit -am "$(git diff | fabric -p create_git_diff_commit_message -m deepseek-coder-v2:16b)" || git reset --hard)

```
