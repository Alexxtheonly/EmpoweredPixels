#!/bin/sh
#author		:Alexander Herold

if [ -z "$1" ]; then
	echo "path must be set"
	exit 1
fi

result=$(curl -o - -s -w "%{http_code}\n" "$1")

if [ "$result" = 200 ]; then
	echo "Ok."
	exit 0
else
	echo "Result was $result"
	exit 1
fi
