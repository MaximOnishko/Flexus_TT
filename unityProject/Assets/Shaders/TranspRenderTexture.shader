Shader "Custom/TranspRenderTexture"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _RenderTex ("Render Texture", 2D) = "black" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        ZWrite Off        // Prevents writing to the depth buffer
        ZTest LEqual      // Ensures proper depth sorting
        Blend SrcAlpha OneMinusSrcAlpha  // Standard alpha blending

        CGPROGRAM
        #pragma surface surf Standard alpha:blend fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _RenderTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float2 flippedUV = float2(IN.uv_MainTex.x, 1.0 - IN.uv_MainTex.y);

            fixed4 baseColor = tex2D(_MainTex, flippedUV) * _Color;
            fixed4 renderColor = tex2D(_RenderTex, flippedUV);

            // Blend render texture on top of main texture
            float alphaBlend = renderColor.a;
            fixed4 finalColor = lerp(baseColor, renderColor, alphaBlend);

            o.Albedo = finalColor.rgb;
            o.Alpha = finalColor.a;

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Transparent"
}