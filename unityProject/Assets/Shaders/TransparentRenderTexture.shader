Shader "Custom/TransparentRenderTexture"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _RenderTex ("Render Texture", 2D) = "black" {}
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent" "RenderType"="Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha // Enables transparency
        ZWrite Off // Prevents depth issues
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _RenderTex;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 flippedUV = float2(i.uv.x, 1.0 - i.uv.y);
                
                fixed4 baseColor = tex2D(_MainTex, flippedUV);
                fixed4 renderColor = tex2D(_RenderTex, flippedUV);

                // Only show renderColor where it's drawn, otherwise show baseColor
                return lerp(baseColor, renderColor, renderColor.a);
            }
            ENDCG
        }
    }
}