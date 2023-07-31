#! /usr/bin/bash
# This is a comment
# pwd
# whoami
#sleep 1
projectsUKnackBasis='/c/Projects/UKnack/UKnackBasis/'
githubUKnackBasis='/c/GitHub/UKnackSource/UKnackBasis/'
#mkdir -p $githubUKnackBasis
#rm --verbose -r $githubUKnackBasis*
#sleep 1
#read -p "Press enter to continue"
#githubUKnackEvents='/c/GitHub/UKnackSource/UKnackBasis/Events/'
#mkdir -p $githubUKnackEvents
#rm --verbose -r $githubUKnackEvents*

#read -p "Press enter to continue2"
#sleep 10
#rm --verbose -r /c/GitHub/UKnack/source/UKnackBasis/Attributes/*

PARAMS=(
    -rv
    --exclude Directory.Build.props
    --exclude UKnackBasis.csproj
)

rsync ${PARAMS[@]} $projectsUKnackBasis $githubUKnackBasis


projectsUKnackBasisConcrete='/c/Projects/UKnack/UKnackBasisConcrete/'
githubUKnackBasisConcrete='/c/GitHub/UKnackSource/UKnackBasisConcrete/'
PARAMS=(
    -rv
    --exclude UKnackBasisConcrete.csproj
    --exclude .vs/
    --exclude bin/
    --exclude obj/
)

rsync ${PARAMS[@]} $projectsUKnackBasisConcrete $githubUKnackBasisConcrete

###################

projectUKnackPro='/c/Projects/UKnack/UKnackPro/'
githubProSource='/c/GitHub/UKnackPro/SourceUKnackPro/'

PARAMS=(
    -rv
    --exclude Directory.Build.props
    --exclude UKnackPro.csproj
)
rsync ${PARAMS[@]} $projectUKnackPro $githubProSource

##################

projectUKnackProConcrete='/c/Projects/UKnack/UKnackProConcrete/'

githubProConcretePackage='/c/GitHub/UKnackPro/Packages/com.atomknack.uknackpro/UKnackProConcrete/'
PARAMS=(
    -rv
    --exclude Directory.Build.props
    --exclude UKnackProConcrete.csproj
    --exclude .vs/
    --exclude bin/
    --exclude obj/
)
rsync ${PARAMS[@]} $projectUKnackProConcrete $githubProConcretePackage


#cp --verbose -a /c/Projects/UKnack/UKnackBasis/Events/. $githubUKnackEvents

#sleep 10
sleep 1