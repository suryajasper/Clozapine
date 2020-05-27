// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#include "Fort_TassionGameMode.h"
#include "Fort_TassionCharacter.h"
#include "UObject/ConstructorHelpers.h"

AFort_TassionGameMode::AFort_TassionGameMode()
{
	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnBPClass(TEXT("/Game/ThirdPersonCPP/Blueprints/ThirdPersonCharacter"));
	if (PlayerPawnBPClass.Class != NULL)
	{
		DefaultPawnClass = PlayerPawnBPClass.Class;
	}
}
