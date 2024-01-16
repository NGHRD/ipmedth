Shader "Custom/AutoScalingTextureShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScaleFactor ("Scale Factor", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
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
            float _ScaleFactor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Scale the texture coordinates
                o.uv = v.uv * _ScaleFactor;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture with scaled UVs
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
