#!/bin/bash
NEWDAY=$1
NEWDAY_INT=$(expr $1 + 0)
PROJECT_NAME=$2
YEAR=2022

if [ -z $1 ]; then
	echo "Please Enter a Day Number"
	exit
fi

cd $NEWDAY/input/

if [ -z "${YULIVEE_AOC_SESSION_COOKIE+x}" ]; then
    echo "You need to set yulivees session cookie in order to download her input. Use ENV Var YULIVEE_AOC_SESSION_COOKIE";
else
	curl "https://adventofcode.com/$YEAR/day/$NEWDAY_INT/input" -H "Cookie: session=$YULIVEE_AOC_SESSION_COOKIE" -o input_yulivee.txt;
fi

if [ -z ${XEMROX_AOC_SESSION_COOKIE+x} ]; then
    echo "You need to set xemrox' session cookie in order to download his input. Use ENV Var XEMROX_AOC_SESSION_COOKIE";
else
	curl "https://adventofcode.com/$YEAR/day/$NEWDAY_INT/input" -H "Cookie: session=$XEMROX_AOC_SESSION_COOKIE" -o input_xemrox.txt;
fi