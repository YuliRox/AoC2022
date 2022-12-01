#!/bin/bash
NEWDAY=$1
PROJECT_NAME=$2

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