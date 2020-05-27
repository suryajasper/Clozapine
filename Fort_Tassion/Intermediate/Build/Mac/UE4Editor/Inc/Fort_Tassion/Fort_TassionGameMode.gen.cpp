// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.
/*===========================================================================
	Generated code exported from UnrealHeaderTool.
	DO NOT modify this manually! Edit the corresponding .h files instead!
===========================================================================*/

#include "UObject/GeneratedCppIncludes.h"
#include "Fort_Tassion/Fort_TassionGameMode.h"
#ifdef _MSC_VER
#pragma warning (push)
#pragma warning (disable : 4883)
#endif
PRAGMA_DISABLE_DEPRECATION_WARNINGS
void EmptyLinkFunctionForGeneratedCodeFort_TassionGameMode() {}
// Cross Module References
	FORT_TASSION_API UClass* Z_Construct_UClass_AFort_TassionGameMode_NoRegister();
	FORT_TASSION_API UClass* Z_Construct_UClass_AFort_TassionGameMode();
	ENGINE_API UClass* Z_Construct_UClass_AGameModeBase();
	UPackage* Z_Construct_UPackage__Script_Fort_Tassion();
// End Cross Module References
	void AFort_TassionGameMode::StaticRegisterNativesAFort_TassionGameMode()
	{
	}
	UClass* Z_Construct_UClass_AFort_TassionGameMode_NoRegister()
	{
		return AFort_TassionGameMode::StaticClass();
	}
	struct Z_Construct_UClass_AFort_TassionGameMode_Statics
	{
		static UObject* (*const DependentSingletons[])();
#if WITH_METADATA
		static const UE4CodeGen_Private::FMetaDataPairParam Class_MetaDataParams[];
#endif
		static const FCppClassTypeInfoStatic StaticCppClassTypeInfo;
		static const UE4CodeGen_Private::FClassParams ClassParams;
	};
	UObject* (*const Z_Construct_UClass_AFort_TassionGameMode_Statics::DependentSingletons[])() = {
		(UObject* (*)())Z_Construct_UClass_AGameModeBase,
		(UObject* (*)())Z_Construct_UPackage__Script_Fort_Tassion,
	};
#if WITH_METADATA
	const UE4CodeGen_Private::FMetaDataPairParam Z_Construct_UClass_AFort_TassionGameMode_Statics::Class_MetaDataParams[] = {
		{ "HideCategories", "Info Rendering MovementReplication Replication Actor Input Movement Collision Rendering Utilities|Transformation" },
		{ "IncludePath", "Fort_TassionGameMode.h" },
		{ "ModuleRelativePath", "Fort_TassionGameMode.h" },
		{ "ShowCategories", "Input|MouseInput Input|TouchInput" },
	};
#endif
	const FCppClassTypeInfoStatic Z_Construct_UClass_AFort_TassionGameMode_Statics::StaticCppClassTypeInfo = {
		TCppClassTypeTraits<AFort_TassionGameMode>::IsAbstract,
	};
	const UE4CodeGen_Private::FClassParams Z_Construct_UClass_AFort_TassionGameMode_Statics::ClassParams = {
		&AFort_TassionGameMode::StaticClass,
		"Game",
		&StaticCppClassTypeInfo,
		DependentSingletons,
		nullptr,
		nullptr,
		nullptr,
		UE_ARRAY_COUNT(DependentSingletons),
		0,
		0,
		0,
		0x008802ACu,
		METADATA_PARAMS(Z_Construct_UClass_AFort_TassionGameMode_Statics::Class_MetaDataParams, UE_ARRAY_COUNT(Z_Construct_UClass_AFort_TassionGameMode_Statics::Class_MetaDataParams))
	};
	UClass* Z_Construct_UClass_AFort_TassionGameMode()
	{
		static UClass* OuterClass = nullptr;
		if (!OuterClass)
		{
			UE4CodeGen_Private::ConstructUClass(OuterClass, Z_Construct_UClass_AFort_TassionGameMode_Statics::ClassParams);
		}
		return OuterClass;
	}
	IMPLEMENT_CLASS(AFort_TassionGameMode, 2176745918);
	template<> FORT_TASSION_API UClass* StaticClass<AFort_TassionGameMode>()
	{
		return AFort_TassionGameMode::StaticClass();
	}
	static FCompiledInDefer Z_CompiledInDefer_UClass_AFort_TassionGameMode(Z_Construct_UClass_AFort_TassionGameMode, &AFort_TassionGameMode::StaticClass, TEXT("/Script/Fort_Tassion"), TEXT("AFort_TassionGameMode"), false, nullptr, nullptr, nullptr);
	DEFINE_VTABLE_PTR_HELPER_CTOR(AFort_TassionGameMode);
PRAGMA_ENABLE_DEPRECATION_WARNINGS
#ifdef _MSC_VER
#pragma warning (pop)
#endif
