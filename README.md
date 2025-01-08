# Domino Circle Validator

A C# console application that determines if a given sequence of domino tiles can form a circular chain.

## Description

This application validates whether a given sequence of domino numbers can form a circular chain, where the numbers at connecting ends match and the last domino connects back to the first one.

## Features

- Validates input format for domino sequences
- Determines if dominos can form a valid circular chain
- Handles both valid and invalid input cases
- Console-based interface for easy testing

## Usage
run project
```bash
dotnet run --project Domino
```
run unit tests (remove logger command to reduce information)
```bash
dotnet test --logger "console;verbosity=detailed" 
```

Enter the domino sequence when prompted. Format: [1|2] [2|3] [3|1]

### Input Format
- Each domino is represented as [x|y] where x and y are numbers
- Dominos are written consecutively with spaces between stones
- Example: [1|2] [2|3] [3|1]

### Output
- Returns "- Is circular? - True" if the sequence can form a circle
- Returns "- Is circular? - False" if the sequence cannot form a circle
- Returns one of the possible solutions to show the valid from
- Throws an exception if the input is something unexpected. The program will end

### Examples

```
Input: [1|2] [2|3] [3|1] [2|2] [2|2]
Output: 
- Is circular? - True
- Lets see a valid solution - [1|2] [2|2] [2|2] [2|3] [3|1]

Input: [1|2] [3|4]
Output: 
- Is circular? - False
- Lets see a valid solution - This game is not circular, please try with a different game
```

## Technical Requirements

- .NET 8.0 or higher
- C#

## Installation

1. Clone the repository
2. Navigate to project directory
3. Run `dotnet run --project Domain` (or run it inside visual studio)