Shader "Custom/Personagens"
{
    Properties
    {
	    _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Perlim ("Perlim (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
		_CorEspecular ("Cor Especular", Color) = (1,1,1,1)
		_CorAmbiente ("Cor ambiente", Color) = (1,1,1,1)
		_CorRim ("Cor Rim", Color) = (1,1,1,1)
		_QuantidadeRim ("Quantidade Rim", Float) = 1
		_TamanhoRim ("Tamanho Rim", Float) = 1
        _Glossiness ("Tamanho Especular", Range(0,1)) = 0.5
		_CorOutline("Cor Outline", color) = (1,1,1,1)
		_ProfundidadeOutline("Profundidade Outline", Range(0,1)) = 0
		_Angulo("Angulo de mudanca", Range(0.0, 180.0)) = 89

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Toon fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 viewDir;
        };

        half _Glossiness;
        half _QuantidadeRim;
		float _TamanhoRim;
        fixed4 _Color;
		fixed4 _CorAmbiente;
		fixed4 _CorEspecular;
		fixed4 _CorRim;
		

		float4 LightingToon(SurfaceOutput s, half3 lightDir, half3 viewDir, float atten){

			// Difuso
			half NdotL = dot (s.Normal, lightDir);
			half Mudanca = fwidth(NdotL);
			half FinalDifuso = smoothstep(0,Mudanca,NdotL);

			//Especular
			half3 reflectionDir = reflect(lightDir,s.Normal);
			half NdotH = dot(viewDir ,-reflectionDir);
			half MudancaEspecular = fwidth(NdotH);
			half4 FinalEspecular = smoothstep(1 - _Glossiness, 1 - _Glossiness + MudancaEspecular, NdotH) * _CorEspecular;

			//Calculo Final
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (FinalDifuso + _CorAmbiente + FinalEspecular) * atten;
			c.a = s.Alpha;
			return c;
		}


        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
     
        }
        ENDCG

		 Cull Front
         CGPROGRAM
         #pragma surface surf Lambert vertex:vert
 
         sampler2D _MainTex;
		 sampler2D _Perlim;
		 float _Angulo;
		 float _ProfundidadeOutline;
		 float4 _CorOutline;
 
         struct Input {
             float2 uv_MainTex;

         };

			void vert(inout appdata_full v){


				float3 scaleDir = normalize(v.vertex.xyz - float4(0,0,0,1));
			   	if (degrees(acos(dot(scaleDir.xyz, v.normal.xyz))) > _Angulo) {
					v.vertex.xyz += normalize(v.normal.xyz) * _ProfundidadeOutline;
				}else {
					v.vertex.xyz += scaleDir * _ProfundidadeOutline;
				}
			}


			void surf (Input IN, inout SurfaceOutput o) {

               o.Albedo = _CorOutline;
            }

         ENDCG
		
    }
    FallBack "Diffuse"
}
