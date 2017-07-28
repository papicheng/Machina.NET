﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRobot
{

    /// <summary>
    /// Represents a Coordinate System composed of a triplet of orthogonal XYZ unit vectors
    /// following right-hand rule orientations. Useful for spatial and rotational orientation
    /// operations. 
    /// </summary>
    public class NuCoordinateSystem : Geometry
    {
        /**
         * NOTE: just as rotation, this class is just a wrapper around the underlying 
         * rotational elements that represent the orientation of this Coordinate System in space, 
         * namely a Quaternion.
         * The main purpose of this class it to be an intuitive way of representing orientation 
         * in three-dimensional space. AxisAngle is therefore not used here, since conceptually
         * there is no need to represent rotations or store overturns in an object that represents
         * pure orientation. 
         * Typical inputs will be vectors in space or conversions from other rotation representations, 
         * and typical visual outputs will be main Vectors, Rotation Matrices or Euler Angles 
         * (even though all internal computation is based on Quaternion algebra). 
         **/

        internal Quaternion Q = null;
        internal RotationMatrix RM = null;
        
        /// <summary>
        /// The main X direction of this Coordinate System.
        /// </summary>
        public Vector XAxis
        {
            get
            {
                return this.RM == null ? 
                    new Vector(1, 0, 0) : 
                    new Vector(this.RM.m00, this.RM.m10, this.RM.m20);
            }
        }

        /// <summary>
        /// The main Y direction of this Coordinate System.
        /// </summary>
        public Vector YAxis
        {
            get
            {
                return this.RM == null ? 
                    new Vector(0, 1, 0) : 
                    new Vector(this.RM.m01, this.RM.m11, this.RM.m21);
            }
        }

        /// <summary>
        /// The main Y direction of this Coordinate System.
        /// </summary>
        public Vector ZAxis
        {
            get
            {
                return this.RM == null ? 
                    new Vector(0, 0, 1) : 
                    new Vector(this.RM.m02, this.RM.m12, this.RM.m22);
            }
        }

        /// <summary>
        /// Create an empty CS object.
        /// </summary>
        internal NuCoordinateSystem() { }

        /// <summary>
        /// Create a CoordinateSystem from the main X and Y axes.
        /// This constructor will create the best-fit orthogonal coordinate system
        /// respecting the direction of the X vector and the plane formed with the Y vector. 
        /// The Z vector will be normal to this planes, and all vectors will be unitized. 
        /// </summary>
        /// <param name="vectorX"></param>
        /// <param name="vectorY"></param>
        public NuCoordinateSystem(Vector vectorX, Vector vectorY) 
            : this(vectorX.X, vectorX.Y, vectorX.Z, vectorY.X, vectorY.Y, vectorY.Z) { }

        /// <summary>
        /// Create a CoordinateSystem from the components of the main X and Y axes.
        /// This constructor will create the best-fit orthogonal coordinate system
        /// respecting the direction of the X vector and the plane formed with the Y vector. 
        /// The Z vector will be normal to this planes, and all vectors will be unitized. 
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y0"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        public NuCoordinateSystem(double x0, double x1, double x2, double y0, double y1, double y2)
        {
            this.RM = new RotationMatrix(x0, x1, x2, y0, y1, y2);
            this.Q = this.RM.ToQuaternion();
        }

        /// <summary>
        /// Create a CS from a Quaternion representation.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        internal NuCoordinateSystem(Quaternion q)
        {
            this.Q = new Quaternion(q);
            this.RM = this.Q.ToRotationMatrix();
        }

        /// <summary>
        /// Creates a CS from a Rotation representation.
        /// </summary>
        /// <param name="r"></param>
        internal NuCoordinateSystem(Rotation r)
            : this(r.Q) { }


        public override string ToString()
        {
            return string.Format("[X:[{0}, {1}, {2}], Y:[{3}, {4}, {5}], Z:[{6}, {7}, {8}]]",
                Math.Round(this.RM.m00, STRING_ROUND_DECIMALS_MM), Math.Round(this.RM.m10, STRING_ROUND_DECIMALS_MM), Math.Round(this.RM.m20, STRING_ROUND_DECIMALS_MM),
                Math.Round(this.RM.m01, STRING_ROUND_DECIMALS_MM), Math.Round(this.RM.m11, STRING_ROUND_DECIMALS_MM), Math.Round(this.RM.m21, STRING_ROUND_DECIMALS_MM),
                Math.Round(this.RM.m02, STRING_ROUND_DECIMALS_MM), Math.Round(this.RM.m12, STRING_ROUND_DECIMALS_MM), Math.Round(this.RM.m22, STRING_ROUND_DECIMALS_MM));
        }

    }
}
