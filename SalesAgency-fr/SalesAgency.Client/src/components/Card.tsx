import type React from "react";

export interface CardProps {
  title: string;
  action?: React.ReactNode;
  children: React.ReactNode;
}

export default function Card({ title, action, children }: CardProps) {
  return (
    <div>
      <div className="flex items-center py-1">
        <div className="text-zinc-500 uppercase flex-1 text-xs">{title}</div>
        {action}
      </div>
      <div className="bg-white py-2">
        {children}
      </div>
    </div>
  )
}