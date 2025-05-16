import type React from "react";

export interface PageProps {
  title: string;
  primaryAction?: React.ReactNode;
  children: React.ReactNode;
}


export function Page({ title, primaryAction, children }: PageProps) {
  return (
    <div>
      <div className="flex items-center flex-row">
        <h2 className="uppercase text-sm text-zinc-500 flex-1">{title}</h2>
        <div>
          {primaryAction}
        </div>
      </div>
      <div className="py-4">
        {children}
      </div>
    </div>
  )
}