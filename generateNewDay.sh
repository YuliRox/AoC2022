#!/bin/bash
NEWDAY=$1
PROJECT_NAME=$2
YEAR=2022

if [ -z $1 ]; then
	echo "Please Enter a Day Number"
	exit
fi

if [ -z $2 ]; then
	echo "Please Enter a Project Name"
	exit
fi

cp -r template $NEWDAY
sed -i "s/src/$2/g" $NEWDAY/src/Properties/launchSettings.json
sed -i "s/src/$2/g" $NEWDAY/.vscode/launch.json
sed -i "s/src/$2/g" $NEWDAY/.vscode/tasks.json
sed -i "s/src/$2/g" $NEWDAY/src/Program.cs
mv $NEWDAY/src $NEWDAY/$2
mv $NEWDAY/$2/src.csproj $NEWDAY/$2/$2.csproj
dotnet sln add $NEWDAY/$2/$2.csproj

DAY_INT=$(expr $NEWDAY + 0) #bash version of str to int
if [ -z ${YULIVEE_AOC_SESSION_COOKIE+x} ]; then
	cd $NEWDAY/input/
	curl "https://adventofcode.com/$YEAR/day/$NEWDAY_INT/input" -H "Cookie: session=$YULIVEE_AOC_SESSION_COOKIE" -o input_yulivee.txt;
fi

if [ -z ${XEMROX_AOC_SESSION_COOKIE+x} ]; then
	cd $NEWDAY/input/
	curl "https://adventofcode.com/$YEAR/day/$NEWDAY_INT/input" -H "Cookie: session=$XEMROX_AOC_SESSION_COOKIE" -o input_xemrox.txt;
fi