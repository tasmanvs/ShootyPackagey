Shader "Custom/TestCurved" 
{
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Degree("Curve Degree",float) = 50
        _Randomness("Curve direction", float) = 0
    }
    
    SubShader {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Randomness;
            float _Degree;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv: TEXCOORD0;
            };

            struct v2f {
                float2 uv  : TEXCOORD0;
                float4 vertex: SV_POSITION;
            };

             v2f vert (appdata_full v)
            {
               v2f o;
               float4 vPos = mul (UNITY_MATRIX_MV, v.vertex);
               float zOff = vPos.z/_Degree;
                    vPos += float4(-15 * _Randomness,0,0,0)*zOff*zOff;
               o.vertex = mul (UNITY_MATRIX_P, vPos);
               o.uv = v.texcoord;
               return o;
            }

            half4 frag (v2f i) : COLOR
            {
                half4 col = tex2D(_MainTex, i.uv.xy);
                return col;
            }
            ENDCG
        }
    }
    
    FallBack "Diffuse"
}