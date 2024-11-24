using System;
using System.Collections.Generic;
using UnityEngine;

namespace RayFire
{
    [SelectionBase]
    [AddComponentMenu ("RayFire/Rayfire Debris")]
    [HelpURL ("https://rayfirestudios.com/unity-online-help/components/unity-debris-component/")]
    public class RayfireDebris : MonoBehaviour
    {
        // UI
        public bool                      onDemolition;
        public bool                      onActivation;
        public bool                      onImpact;
        public GameObject                debrisReference;
        public Material                  debrisMaterial;
        public Material                  emissionMaterial;
        public RFParticleEmission        emission;
        public RFParticleDynamicDebris   dynamic;
        public RFParticleNoise           noise;
        public RFParticleCollisionDebris collision;
        public RFParticleLimitations     limitations;
        public RFParticleRendering       rendering;
        
        // Non serialized
        [NonSerialized] public bool                initialized;
        [NonSerialized] public RayfireRigid        rigid;
        [NonSerialized] public Mesh[]              meshes;
        [NonSerialized] public ParticleSystem      pSystem;
        [NonSerialized] public Transform           hostTm;
        [NonSerialized] public List<RayfireDebris> children;
        [NonSerialized] public int                 amountFinal;
        [NonSerialized] public bool                oldChild;
        [NonSerialized]        Vector3             refScale;
        
        // Static
        static Renderer     rn;
        static MeshFilter[] mar;

        /// /////////////////////////////////////////////////////////
        /// Common
        /// /////////////////////////////////////////////////////////
        
        // Constructor
        public RayfireDebris()
        {
            onDemolition     = false;
            onActivation     = false;
            onImpact         = false;
            debrisReference  = null;
            debrisMaterial   = null;
            emissionMaterial = null;
            emission         = new RFParticleEmission();
            dynamic          = new RFParticleDynamicDebris();
            noise            = new RFParticleNoise();
            collision        = new RFParticleCollisionDebris();
            limitations      = new RFParticleLimitations();
            rendering        = new RFParticleRendering();
            refScale         = Vector3.one;
            amountFinal      = 5;
        }

        // Copy from
        public void CopyFrom(RayfireDebris source)
        {
            onDemolition     = source.onDemolition;
            onActivation     = source.onActivation;
            onImpact         = source.onImpact;
            debrisReference  = source.debrisReference;
            debrisMaterial   = source.debrisMaterial;
            emissionMaterial = source.emissionMaterial;
            emission.CopyFrom (source.emission);
            dynamic.CopyFrom (source.dynamic);
            noise.CopyFrom (source.noise);
            collision.CopyFrom (source.collision);
            limitations.CopyFrom (source.limitations);
            rendering.CopyFrom (source.rendering);
            refScale = source.refScale;

            // Hidden
            meshes         = source.meshes;
            initialized    = source.initialized;
        }

        /// /////////////////////////////////////////////////////////
        /// Methods
        /// ///////////////////////////////////////////////////////// 
        
        // Initialize
        public void Initialize()
        {
            // Do not initialize if already initialized or parent was initialized
            if (initialized == true)
                return;

            // Set debris ref meshes
            SetReferenceMeshes (debrisReference);
        }
        
        // Emit particles
        public ParticleSystem Emit()
        {
            // Initialize
            Initialize();
            
            // Emitter is not ready
            if (initialized == false)
                return null;

            // Set material properties in case object has no rigid
            collision.SetMaterialProps (this);
            
            // Particle system
            ParticleSystem ps = RFParticles.CreateParticleSystemDebris(this, transform);

            // Get components
            MeshFilter emitMeshFilter = GetComponent<MeshFilter>();
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            // Get emit material index
            int emitMatIndex = RFParticles.GetEmissionMatIndex (meshRenderer, emissionMaterial);

            // Set amount
            amountFinal = emission.burstAmount;
            
            // Create debris
            CreateDebris(this, emitMeshFilter, emitMatIndex, ps);

            return ps;
        }
        
        // Clean particle systems
        public void Clean()
        {
            // Destroy own particles
            if (hostTm != null)
                Destroy (hostTm.gameObject);

            // Destroy particles on children debris
            if (HasChildren == true)
                for (int i = 0; i < children.Count; i++)
                    if (children[i] != null)
                        if (children[i].hostTm != null)
                            Destroy (children[i].hostTm.gameObject);
        }
        
        /// /////////////////////////////////////////////////////////
        /// Create common
        /// /////////////////////////////////////////////////////////
        
        // Create single debris particle system
        public static void CreateDebris(RayfireDebris scr, MeshFilter emitMeshFilter, int emitMatIndex, ParticleSystem ps)
        {
            // Set main module
            RFParticles.SetMain(ps.main, scr.emission.lifeMin, scr.emission.lifeMax, scr.emission.sizeMin, scr.emission.sizeMax, 
                scr.dynamic.gravityMin, scr.dynamic.gravityMax, scr.dynamic.speedMin, scr.dynamic.speedMax, 
                3.1f, scr.limitations.maxParticles, scr.emission.duration);

            // Emission over distance
            RFParticles.SetEmission(ps.emission, scr.emission.distanceRate, scr.amountFinal);

            // Emission from mesh or from impact point
            if (emitMeshFilter != null)
                RFParticles.SetShapeMesh(ps.shape, emitMeshFilter.sharedMesh, emitMatIndex, emitMeshFilter.transform.localScale);
            else
                RFParticles.SetShapeObject(ps.shape);

            // Inherit velocity 
            RFParticles.SetVelocity(ps.inheritVelocity, scr.dynamic);
            
            // Size over lifetime
            RFParticles.SetSizeOverLifeTime(ps.sizeOverLifetime, scr.refScale);

            // Rotation by speed
            RFParticles.SetRotationBySpeed(ps.rotationBySpeed, scr.dynamic.rotationSpeed);

            // Collision
            RFParticles.SetCollisionDebris(ps.collision, scr.collision);

            // Noise
            RFParticles.SetNoise (ps.noise, scr.noise);
            
            // Renderer
            RFParticles.SetParticleRendererDebris(ps.GetComponent<ParticleSystemRenderer>(), scr);

            // Start playing
            ps.Play();
        }
        
        /// /////////////////////////////////////////////////////////
        /// Renderer
        /// /////////////////////////////////////////////////////////
        
        // Get reference meshes
        void SetReferenceMeshes(GameObject refs)
        {
            // No reference. Use own mesh
            if (refs == null)
            {
                Debug.Log ("RayFire Debris: " + gameObject.name + ": Debris reference not defined.", gameObject);
                return;
            }
            
            // Add local mf
            if (refs.transform.childCount > 0)
            {
                mar = refs.GetComponentsInChildren<MeshFilter>();
            }
            else if (mar == null || mar[0] == null)
            {
                mar = new MeshFilter[1]{refs.GetComponent<MeshFilter>()};
            }

            // No mesh filters
            if (mar.Length == 0)
            {
                Debug.Log ("RayFire Debris: " + gameObject.name + ": Debris reference mesh is not defined.", gameObject);
                return;
            }

            // Get all meshes
            meshes = new Mesh[4];
            for (int i = 0; i < mar.Length; i++)
            {
                // Limit by 4. Particle system can't take mor than 4 ref meshes
                if (i == 4)
                    break;

                if (mar[i].sharedMesh != null && mar[i].sharedMesh.vertexCount > 3)
                    meshes[i] = mar[i].sharedMesh;
                else
                    Debug.Log ("RayFire Debris: " + mar[i].name + ": has no mesh or amount of vertices too low.", gameObject);
            }

            // Set debris material
            SetDebrisMaterial (mar);
            
            // Set scale
            if (mar[0] != null)
                refScale = mar[0].transform.lossyScale;
            initialized = true;
            mar         = null;
        }
        
        // Set debris material
        void SetDebrisMaterial(MeshFilter[] mfs)
        {
            // Already defined
            if (debrisMaterial != null)
                return;
            
            for (int i = 0; i < mfs.Length; i++)
            {
                rn = mfs[i].GetComponent<Renderer>();
                if (rn != null)
                {
                    if (rn.sharedMaterial != null)
                    {
                        debrisMaterial = rn.sharedMaterial;
                        return;
                    }
                }
            }

            // Set original object material
            if (debrisMaterial == null)
                debrisMaterial = GetComponent<Renderer>().sharedMaterial;
            rn = null;
        }

        public bool HasChildren { get { return children != null && children.Count > 0; } }
    }
}
