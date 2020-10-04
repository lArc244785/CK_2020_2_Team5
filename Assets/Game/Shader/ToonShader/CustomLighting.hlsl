#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED
#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
//MainLight_half
void MainLight_half(float3 worldPos, out half3 lightDir, out half3 lightColor0, out half distanceAtten, out half shadowAtten)
{
#if SHADERGRAPH_PREVIEW			//! ���̴� �׷��� �����信�� ���̴� ���
    lightDir = half3(0.5, 0.5, 0);
    lightColor0 = 1;
    distanceAtten = 1;
    shadowAtten = 1;
#else

#if SHADOWS_SCREEN	//! Screen Space Shadow ����Ҷ� (NoȮ��)
    half4 clipPos = TransformWorldToHClip(worldPos);
    half4 shadowCoord = ComputeScreenPos(clipPos);
#else
    half4 shadowCoord = TransformWorldToShadowCoord(worldPos);	//! ���� ������ǥ�� �׸��� ���� ��ǥ�� ��ȯ�ϴ� �Լ� (NoȮ��)
#endif

    Light mainLight = GetMainLight();			//! Lighting.hlsl �Լ��� ����Ʈ ���� �� ������ ������ ����ü ����
    lightDir = normalize(mainLight.direction);	//! ���� ����Ʈ ����
    lightColor0 = mainLight.color;				//! ���� ����Ʈ Į��

    distanceAtten = mainLight.distanceAttenuation;	//! �ø�����ũ�� ���� �ø��Ǹ� 1, �ƴϸ� 0 (NoȮ��), ����Ʈ�� ��Ȳ���� �ٸ�
    //shadowAtten = mainLight.shadowAttenuation;		//! <
    ShadowSamplingData shadowSamplingData = GetMainLightShadowSamplingData();	//! ������ ���谪
    half4 shadowParams = GetMainLightShadowParams();
    shadowAtten = SampleShadowmap(TEXTURE2D_ARGS(_MainLightShadowmapTexture, sampler_MainLightShadowmapTexture), TransformWorldToShadowCoord(worldPos), shadowSamplingData, shadowParams, false);
#endif
}

void DirectSpecular_half(half3 Specular, half Smoothness, half3 Direction, half3 Color, half3 WorldNormal, half3 WorldView, out half3 Out)
{
#if SHADERGRAPH_PREVIEW
    Out = 0;
#else
    Smoothness = exp2(10 * Smoothness + 1);
    WorldNormal = normalize(WorldNormal);
    WorldView = SafeNormalize(WorldView);
    Out = LightingSpecular(Color, Direction, WorldNormal, WorldView,half4(Specular, 0), Smoothness);
#endif
}


void DirectSpecular_float(half3 Specular, half Smoothness, half3 Direction, half3 Color, half3 WorldNormal, half3 WorldView, out half3 Out)
{
#if SHADERGRAPH_PREVIEW
    Out = 0;
#else
    Smoothness = exp2(10 * Smoothness + 1);
    WorldNormal = normalize(WorldNormal);
    WorldView = SafeNormalize(WorldView);
    Out = LightingSpecular(Color, Direction, WorldNormal, WorldView, half4(Specular, 0), Smoothness);
#endif
}

void AdditionalLights_half(half3 SpecColor, half Smoothness, half3 WorldPosition, half3 WorldNormal, half3 WorldView, out half3 Diffuse, out half3 Specular)
{
    half3 diffuseColor = 0;
    half3 specularColor = 0;

#ifndef SHADERGRAPH_PREVIEW
    Smoothness = exp2(10 * Smoothness + 1);
    WorldNormal = normalize(WorldNormal);
    WorldView = SafeNormalize(WorldView);
    int pixelLightCount = GetAdditionalLightsCount();
    for (int i = 0; i < pixelLightCount; ++i)
    {
        Light light = GetAdditionalLight(i, WorldPosition);
        half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
        diffuseColor += LightingLambert(attenuatedLightColor, light.direction, WorldNormal);
        specularColor += LightingSpecular(attenuatedLightColor, light.direction, WorldNormal, WorldView, half4(SpecColor, 0), Smoothness);
    }
#endif

    Diffuse = diffuseColor;
    Specular = specularColor;
}

#endif

