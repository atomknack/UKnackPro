#! /usr/bin/bash

sourceConcretePath='/c/GitHub/UKnackSource/UKnackBasisConcrete/'
basisConcretePath='/c/GitHub/UKnackBasis/Packages/com.atomknack.uknackbasis/UKnackBasisConcrete/'
#cp -r $sourceConcretePath/* $basisConcretePath

PARAMS=(
    -rv
    --exclude UKnackBasisConcrete.csproj
    --exclude .vs/
    --exclude bin/
    --exclude obj/
)

rsync ${PARAMS[@]} $sourceConcretePath $basisConcretePath

#read -p "Press enter to continue2"
sleep 1