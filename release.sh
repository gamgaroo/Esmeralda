#!/bin/bash
set -ex

#pull source
git pull

#bump version
docker run --rm -v "$PWD":/app treeder/bump "$(git log -1 --pretty=%B)"
version=`cat VERSION`
echo "version: $version"

#build
./build.sh

#tag
git config --global user.email "builds@travis-ci.org"
git config --global user.name "Travis CI"

git checkout $TRAVIS_BRANCH

git add -A
git commit -m "Version $version"
git tag -a "$version" -m "Version $version"
git push origin $TRAVIS_BRANCH
git push origin $TRAVIS_BRANCH --tags

docker tag $USERNAME/$IMAGE:latest $USERNAME/$IMAGE:$version

#push
echo "$DOCKER_PASSWORD" | docker login -u "$DOCKER_USERNAME" --password-stdin
docker push $USERNAME/$IMAGE:latest
docker push $USERNAME/$IMAGE:$version