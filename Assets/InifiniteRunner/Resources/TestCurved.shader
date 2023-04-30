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

            v2f vert (appdata v)
            {
               v2f o;
               o.uv = TRANSFORM_TEX(v.uv, _MainTex);
               float4 WorldPos = mul(unity_ObjectToWorld, v.vertex);

               float3 ObjToCam = (_WorldSpaceCameraPos - WorldPos.xyz);
               float dist = pow(ObjToCam.z / _Degree, 2);
               WorldPos.x += _Randomness * dist; 
               WorldPos.y -= 0.3 * dist;

               v.vertex = mul(unity_WorldToObject, WorldPos);

               o.vertex = UnityObjectToClipPos(v.vertex);
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